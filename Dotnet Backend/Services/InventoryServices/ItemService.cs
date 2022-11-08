using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;
using users_items_backend.Context;
using DotnetBackend.DTOs;

namespace DotnetBackend.Services.Inventory
{
    public class ItemService : IInventoryService
    {
        public DataContext _repo { get; set; }

        #region Constructor
        public ItemService(DataContext context)
        {
            _repo = context;
        }
        #endregion

        #region Public Async Methods
        public async Task<ServiceResponse<GetItemDTO>> CreateItem(CreateIttemDTO currenItem)
        {

            async Task<GetItemDTO> rawCreation(object obj)
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
                nw.Owner = await _repo.Users.FirstOrDefaultAsync(u => u.Id == nw.UserId);
                await _repo.Items.AddAsync(nw);
                await _repo.SaveChangesAsync();
                return new GetItemDTO(nw);
            }

            return await ServiceHelper<GetItemDTO>.ActionHandler(rawCreation, currenItem);
        }

        public async Task<ServiceResponse<GetItemDTO>> DeleteItem(Guid id)
        {
            async Task<GetItemDTO> rawElimination(object obj)
            {
                var idn = (Guid)obj;
                var old = await _repo.Items.FirstOrDefaultAsync(u => u.Id == idn);
                old = ServiceHelper<InventoryItem>.NoNullsAccepted(old);
                _repo.Items.Remove(old);
                await _repo.SaveChangesAsync();
                return new GetItemDTO(old);
            }
            return await ServiceHelper<GetItemDTO>.ActionHandler(rawElimination, id);
        }

        public async Task<ServiceResponse<GetItemDTO>> GetItem(Guid id)
        {
            async Task<GetItemDTO> rawGetter(object obj)
            {
                var idn = (Guid)obj;
                var requested = await _repo.Items.FirstOrDefaultAsync(u => u.Id == idn);
                return new GetItemDTO(ServiceHelper<InventoryItem>.NoNullsAccepted(requested));
            }

            return await ServiceHelper<GetItemDTO>.ActionHandler(rawGetter, id);
        }

        public async Task<ServiceResponse<List<GetItemDTO>>> GetAllItems()
        {
            async Task<List<GetItemDTO>> rawGet(object a)
            {
                var items = await _repo.Items.ToListAsync();
                var dtoItems = new List<GetItemDTO>();
                items.ForEach(i => dtoItems.Add(new GetItemDTO(i)));
                return dtoItems;
            };
            return await ServiceHelper<List<GetItemDTO>>.ActionHandler(rawGet, 0);

        }

        public async Task<ServiceResponse<GetItemDTO>> UpdateItem(UpdateItemDTO currenItem)
        {
            async Task<GetItemDTO> rawUpdate(object obj)
            {
                var ci = (UpdateItemDTO)obj;
                var old = await _repo.Items.FirstOrDefaultAsync(u => u.Id == ci.ItemId);
                old = ServiceHelper<InventoryItem>.NoNullsAccepted(old);

                old.Quantity = ci.Quantity;
                old.Name = ci.Name;
                old.Description = ci.Description;
                old.UserId = ci.UserId;

                await _repo.SaveChangesAsync();
                return new GetItemDTO(old);
            }
            return await ServiceHelper<GetItemDTO>.ActionHandler(rawUpdate, currenItem);
        }

        #endregion
    }
}