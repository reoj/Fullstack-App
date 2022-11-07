using exam_webapi.Models;

namespace users_items_backend.DTOs.ItemDTOs
{
    public class GetItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }

        public GetItemDTO(InventoryItem it)
        {
            Id = it.Id;
            Name = it.Name;
            Description = it.Description;
            Quantity = it.Quantity;
            UserId = it.UserId;
        }
    }
}
