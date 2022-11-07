using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam_webapi.Models;

namespace exam_webapi.DTOs.UserDTOs
{
    /// <summary>
    /// Data Transfer Object for User update
    /// </summary>
    public class UpdateUserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public UserType_Enum UserType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}