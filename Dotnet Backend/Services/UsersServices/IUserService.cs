using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam_webapi.DTOs.UserDTOs;
using exam_webapi.Models;

namespace exam_webapi.Services.UserServices
{
    public interface IUserService
    {
        User CreateUser(CreateUserDTO nwUser);
        User GetUser(int id);
        User UpdateUser(UserDTO nwUser);
        User DeleteUser(int id);
    }
}