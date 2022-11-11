using DotnetBackend.DTOs;
using DotnetBackend.Models;
using System.Xml.Linq;

namespace users_items_backend.DTOs
{
    /// <summary>
    /// Allows static methods to transform Entities to DTOs and back
    /// </summary>
    public class Mappings
    {
        /// <summary>
        /// Maps an InventoryItem into an UpdateItemDTO
        /// </summary>
        /// <param name="it">Original InventoryItem object</param>
        /// <returns>An UpdateItemDTO object</returns>
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
