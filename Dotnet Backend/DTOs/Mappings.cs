using DotnetBackend.DTOs;
using DotnetBackend.Models;
using System.Xml.Linq;

namespace users_items_backend.DTOs
{
    public class Mappings
    {
        public static UpdateItemDTO AsUpdateItemDTO(InventoryItem it)
        {
            return new UpdateItemDTO()
            {
                ItemId = it.Id,
                Name = it.Name,
                Description = it.Description,
                Quantity = it.Quantity,
                userId = it.UserId,
            };
        }
    }
}
