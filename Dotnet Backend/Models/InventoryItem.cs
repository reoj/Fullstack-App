using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetBackend.Models
{
    /// <summary>
    /// Represents an Item that can be held by a User
    /// </summary>
    public class InventoryItem
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = "Out of Stock";
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public User Owner { get; set; } = null!;
        public int UserId { get; set; }
    }
}