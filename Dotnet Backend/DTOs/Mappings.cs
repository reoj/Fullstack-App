using DotnetBackend.DTOs;
using DotnetBackend.Models;

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
                UserId = it.UserId,
            };
        }

        public static CreateExchangeDTO AsCreateExchangeDTO(Exchange ex)
        {
            return new CreateExchangeDTO()
            {
                Sender = ex.sender.Id,
                Reciever = ex.reciever.Id,
                ItemName = ex.itemName,
                ItemDescription = ex.itemDescription,
                ItemQuantity = ex.itemQuantity,
            };
        }
        public static GetExchangeDTO AsGetEchangeDTO(Exchange ex)
        {
            return new GetExchangeDTO()
            {
                Id = ex.Id,
                Sender = ex.sender.Id,
                Reciever = ex.reciever.Id,
                ItemName = ex.itemName,
                ItemDescription = ex.itemDescription,
                ItemQuantity = ex.itemQuantity,
            };
            
        }
    }
}
