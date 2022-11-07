
using DotnetBackend.Models;
using exam_webapi.DTOs.ItemDTOs;
using users_items_backend.DTOs.ItemDTOs;

namespace exam_webapi.Services.Inventory
{
    public interface IInventoryService
    {
        Task<ServiceResponse<GetItemDTO>> GetItem(Guid id);
        Task<ServiceResponse<List<GetItemDTO>>> GetAllItems();
        Task<ServiceResponse<GetItemDTO>> CreateItem(CreateIttemDTO currenItem);
        Task<ServiceResponse<GetItemDTO>> UpdateItem(UpdateItemDTO currenItem);
        Task<ServiceResponse<GetItemDTO>> DeleteItem(Guid id);
    }
}