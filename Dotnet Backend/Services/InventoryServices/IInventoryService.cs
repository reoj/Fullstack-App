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
    }
}