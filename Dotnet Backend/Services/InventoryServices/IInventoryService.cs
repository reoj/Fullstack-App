
using exam_webapi.DTOs.ItemDTOs;
using exam_webapi.Models;

namespace exam_webapi.Services.Inventory
{
    public interface IInventoryService
    {
        InventoryItem GetItem(Guid id);
        InventoryItem CreateItem (CreateIttemDTO currenItem);
        InventoryItem UpdateItem (UpdateItemDTO currenItem);
        InventoryItem DeleteItem (Guid id);
    }
}