using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tiara_api.DTOs;
using tiara_api.Repository.IRepository;

namespace tiara_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;

        public UsersController(
           IUserRepository _userRepo,
           IMapper _mapper
            )

        {
            userRepo = _userRepo;
            mapper = _mapper;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] UserDto user)
        {
            var userFound = userRepo.Authenticate(user.Email, user.Password);
            if(userFound == null)
            {
                return BadRequest("Username or Password Incorrect!");
            }
            else
            {
                return Ok(userFound);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] UserDto user)
        {
            bool userUnqiue = userRepo.IsUniqueUser(user.Email);
            if (!userUnqiue)
            {
                return BadRequest("User already exists!");
            }
            else
            {
                var newUser = userRepo.Register(user.Email, user.Password);

                if(newUser == null)
                {
                    return BadRequest("Error, unable to register user");
                }

                return Ok(newUser);
            }

        }

    }
}
