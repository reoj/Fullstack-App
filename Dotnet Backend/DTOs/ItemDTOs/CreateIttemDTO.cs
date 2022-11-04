using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exam_webapi.DTOs.ItemDTOs
{
    public class CreateIttemDTO
    {
        public string Name { get; set; } = "Out of Stock";
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}