using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam_webapi.DTOs.UserDTOs;
using exam_webapi.Models;
using exam_webapi.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace exam_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _users;

        public UsersController(IUserService users)
        {
            this._users = users;
        }
        
        [HttpPost]
        public ActionResult<User> CreateUser(CreateUserDTO nwUser){
            return Ok(_users.CreateUser(nwUser));
            
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id){
            return Ok(_users.GetUser(id));
        }
        
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(UserDTO nwUser){
            return Ok(_users.UpdateUser(nwUser));
        }
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id){
            return Ok(_users.DeleteUser(id));
        }

    }
}