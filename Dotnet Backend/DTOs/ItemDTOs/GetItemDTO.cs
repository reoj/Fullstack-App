using exam_webapi.Models;

namespace users_items_backend.DTOs.ItemDTOs
{
    /// <summary>
    /// Represents the Item atributes relevant to the FrontEnd
    /// </summary>
    public class GetItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }

        #region Constructor
        public GetItemDTO(InventoryItem it)
        {
            Id = it.Id;
            Name = it.Name;
            Description = it.Description;
            Quantity = it.Quantity;
            UserId = it.UserId;
        } 
        #endregion
    }
}
