using DotnetBackend.Models;
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
        public async Task<ActionResult<ServiceResponse<User>>> CreateUser(CreateUserDTO nwUser){
            var result = await _users.CreateUser(nwUser);
            return result.Successfull ? Ok(result) : NotFound(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(int id){
            var result = await (_users.GetUser(id));
            return result.Successfull ? Ok(result) : NotFound(result);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<User>>> GetAllUsers(int id)
        {
            var result = await _users.GetAllUsers();
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<User>>> UpdateUser(UpdateUserDTO nwUser){
            var result = await (_users.UpdateUser(nwUser));
            return result.Successfull ? Ok(result) : NotFound(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> DeleteUser(int id){
            var result = await (_users.DeleteUser(id));
            return result.Successfull ? Ok(result) : NotFound(result);
        }
        
    }
}