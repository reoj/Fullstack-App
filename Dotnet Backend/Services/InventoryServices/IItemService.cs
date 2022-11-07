using DotnetBackend.Models;
using DotnetBackend.Services;
using exam_webapi.DTOs.ItemDTOs;
using exam_webapi.Models;
using Microsoft.EntityFrameworkCore;
using users_items_backend.Context;

namespace exam_webapi.Services.Inventory
{
    public class ItemService : IInventoryService
    {
        public DataContext _repo { get; set; }

        public ItemService(DataContext context)
        {
            _repo = context;
        }
        public async Task<ServiceResponse<InventoryItem>> CreateItem(CreateIttemDTO currenItem)
        {

            async Task<InventoryItem> rawCreation(object obj)
            {
                var ci = (CreateIttemDTO)obj;
                InventoryItem nw = new()
                {
                    Id = Guid.NewGuid(),
                    Name = ci.Name,
                    Description = ci.Description,
                    Quantity = ci.Quantity,
                    UserId = ci.UserId,
                };
                await _repo.Items.AddAsync(nw);
                await _repo.SaveChangesAsync();
                return nw;
            }

            return await ServiceHelper<InventoryItem>.ActionHandler(rawCreation, currenItem);
        }

        public async Task<ServiceResponse<InventoryItem>> DeleteItem(Guid id)
        {
            async Task<InventoryItem> rawElimination(object obj)
            {
                var idn = (Guid)obj;
                var old = await _repo.Items.FirstOrDefaultAsync(u => u.Id == idn);
                old = ServiceHelper<InventoryItem>.NoNullsAccepted(old);
                _repo.Remove(old);
                await _repo.SaveChangesAsync();
                return old;
            }
            return await ServiceHelper<InventoryItem>.ActionHandler(rawElimination, id);
        }

        public async Task<ServiceResponse<InventoryItem>> GetItem(Guid id)
        {
            async Task<InventoryItem> rawGetter(object obj)
            {
                var idn = (Guid)obj;
                var requested = await _repo.Items.FirstOrDefaultAsync(u => u.Id == idn);
                return ServiceHelper<InventoryItem>.NoNullsAccepted(requested);
            }

            return await ServiceHelper<InventoryItem>.ActionHandler(rawGetter, id);
        }
        public async Task<ServiceResponse<InventoryItem>> UpdateItem(UpdateItemDTO currenItem)
        {
            async Task<InventoryItem> rawUpdate(object obj)
            {
                var ci = (UpdateItemDTO)obj;
                var old = await _repo.Items.FirstOrDefaultAsync(u => u.Id == ci.ItemId);
                old = ServiceHelper<InventoryItem>.NoNullsAccepted(old);

                old.Quantity = ci.Quantity;
                old.Name = ci.Name;
                old.Description = ci.Description;
                old.UserId = ci.UserId;

                await _repo.SaveChangesAsync();
                return old;
            }
            return await ServiceHelper<InventoryItem>.ActionHandler(rawUpdate, currenItem);
        }
    }
}