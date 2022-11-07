using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam_webapi.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DotnetBackend.DTOs.UserDTOs
{
    /// <summary>
    /// Represents the User atributes relevant to the FrontEnd
    /// </summary>
    public class GetUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public UserType_Enum UserType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<Guid> Items { get; set; }

        #region Constructor
        /// <summary>
        /// Takes a User object and prepares it for data transference
        /// </summary>
        /// <param name="u">Defined User object to be parsed</param>
        public GetUserDTO(User u)
        {
            Name = u.Name;
            UserType = u.UserType;
            Email = u.Email;
            Phone = u.Phone;
            List<Guid> guids = new();
            if (u.items is not null)
            {
                u.items.ForEach(i => guids.Add(i.Id));
            }
            Items = guids;
        } 
        #endregion
    }
}