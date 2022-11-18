using DotnetBackend.Models;

namespace DotnetBackend.DTOs
{
    /// <summary>
    /// Data Transfer Object for Item Update
    /// </summary>
    public class UpdateItemDTO
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; } = "Out of Stock";
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}