using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetBackend.Models;

namespace DotnetBackend.DTOs
{
    /// <summary>
    /// Data Transfer Object for User creation
    /// </summary>
    public class CreateUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public UserType_Enum UserType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}