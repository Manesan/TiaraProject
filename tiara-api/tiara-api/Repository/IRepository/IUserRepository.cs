using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tiara_api.DTOs;
using tiara_api.Models;

namespace tiara_api.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string email);
        User Authenticate(string email, string password);
        User Register(string email, string password);
        User GetUserByToken(string token);
        dynamic GetUserByToken(UserDto user, string token);
    }
}
