
using DotnetBackend.Models;
using exam_webapi.DTOs.ItemDTOs;
using exam_webapi.Models;

namespace exam_webapi.Services.Inventory
{
    public interface IInventoryService
    {
        Task<ServiceResponse<InventoryItem>> GetItem(Guid id);
        Task<ServiceResponse<InventoryItem>> CreateItem(CreateIttemDTO currenItem);
        Task<ServiceResponse<InventoryItem>> UpdateItem(UpdateItemDTO currenItem);
        Task<ServiceResponse<InventoryItem>> DeleteItem(Guid id);
    }
}