
using DotnetBackend.Models;
using exam_webapi.DTOs.ItemDTOs;
using exam_webapi.Models;

namespace exam_webapi.Services.Inventory
{
    public interface IInventoryService
    {
        ServiceResponse<InventoryItem> GetItem(Guid id);
        ServiceResponse<InventoryItem> CreateItem (CreateIttemDTO currenItem);
        ServiceResponse<InventoryItem> UpdateItem (UpdateItemDTO currenItem);
        ServiceResponse<InventoryItem> DeleteItem (Guid id);
    }
}