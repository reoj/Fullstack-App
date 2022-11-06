using DotnetBackend.Models;
using exam_webapi.DTOs.UserDTOs;
using exam_webapi.Models;

namespace exam_webapi.Services.UserServices
{
    public interface IUserService
    {
        ServiceResponse<User> CreateUser(CreateUserDTO nwUser);
        ServiceResponse<User> GetUser(int id);
        ServiceResponse<User> UpdateUser(UserDTO nwUser);
        ServiceResponse<User> DeleteUser(int id);
    }
}