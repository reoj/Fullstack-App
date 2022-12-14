using DotnetBackend.DTOs;
using DotnetBackend.Models;
using DotnetBackend.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBackend.Controllers
{
    /// <summary>
    /// Controller for Users
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _users;

        #region Constructor
        public UsersController(IUserService users)
        {
            this._users = users;
        }
        #endregion

        #region Endpoints
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<User>>> GetAllUsers()
        {
            var result = await _users.GetAllUsers();
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(int id)
        {
            var result = await (_users.GetUser(id));
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<User>>> CreateUser(CreateUserDTO nwUser)
        {
            var result = await _users.CreateUser(nwUser);
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<User>>> UpdateUser(UpdateUserDTO nwUser)
        {
            var result = await (_users.UpdateUser(nwUser));
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> DeleteUser(int id)
        {
            var result = await (_users.DeleteUser(id));
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        #endregion
    }
}