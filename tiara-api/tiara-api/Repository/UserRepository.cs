using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using tiara_api.DataContext;
using tiara_api.DTOs;
using tiara_api.Models;
using tiara_api.Repository.IRepository;

namespace tiara_api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContextDB db;
        private readonly AppSettings appSettings;
        public UserRepository(DataContextDB _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }

        public User Authenticate(string email, string password)
        {
            var encryptedPassword = Encrypt(password);
            var user = db.Users.SingleOrDefault(x => x.Email == email && x.Password == encryptedPassword);

            if(user == null)
            {
                return null;
            }

            else //generate jwt token
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role), //add roll to token

                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                user.Password = "HIDDEN";

                return user;
            }
        }

        public User GetUserByToken(string token)
        {
            var user = db.Users.SingleOrDefault(x => x.Token == token);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }

        public dynamic GetUserByToken(UserDto user, string token)
        {
            var userFound = db.Users.SingleOrDefault(x => x.Token == token);
            if (userFound == null)
            {
                return false;
            }
            else
            {
                if(user.Email == userFound.Email)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsUniqueUser(string email)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == email);
            if(user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User Register(string email, string password)
        {
            var encryptedPassword = Encrypt(password);
            User user = new User()
            {
                Email = email,
                Password = encryptedPassword
            };

            db.Users.Add(user);
            db.SaveChanges();

            user.Password = "HIDDEN";

            return user;
        }


        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}
