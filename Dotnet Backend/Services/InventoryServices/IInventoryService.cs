using DotnetBackend.Models;
using DotnetBackend.DTOs;

namespace DotnetBackend.Services.Inventory
{
    /// <summary>
    /// Contract for Items Service
    /// </summary>
    public interface IInventoryService
    {
        Task<ServiceResponse<GetItemDTO>> GetItem(Guid id);
        Task<ServiceResponse<List<GetItemDTO>>> GetAllItems();
        Task<ServiceResponse<GetItemDTO>> CreateItem(CreateIttemDTO currenItem);
        Task<ServiceResponse<GetItemDTO>> UpdateItem(UpdateItemDTO currenItem);
        Task<ServiceResponse<GetItemDTO>> DeleteItem(Guid id);
        bool IsItemInList(List<InventoryItem> list, string name, string description);
        Task<List<InventoryItem>> GetItemsOfUser(int userId);
        Task<InventoryItem> GetExistingItemRaw(int userid, string name, string description);
    }
}