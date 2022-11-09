using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;
using users_items_backend.Context;
using DotnetBackend.DTOs;
using DotnetBackend.Services.UserServices;

namespace DotnetBackend.Services.Inventory
{
    public class ItemService : IInventoryService
    {
        public DataContext _repo { get; set; }
        public IUserService _uService { get; }

        #region Constructor
        public ItemService(DataContext context, IUserService _uService)
        {
            this._uService = _uService;
            _repo = context;
        }
        #endregion

        #region Public Async Methods
        public async Task<ServiceResponse<GetItemDTO>> CreateItem(CreateIttemDTO currenItem)
        {

            async Task<GetItemDTO> rawCreation(object obj)
            {
                var ci = (CreateIttemDTO)obj;

                // Request the user to the User service
                var targetUser = await _uService.GetUserRaw(ci.UserId);

                // If this Item already exists in the user's inventory, just increase the quantity
                if (IsItemInList(await GetItemsOfUser(ci.UserId), ci.Name, ci.Description))
                {
                    //Request the item from the user's Inventory                    
                    var oldItem = ServiceHelper<List<InventoryItem>>.NoNullsAccepted(
                        targetUser.Items)
                        .Find(i => i.Name == ci.Name && ci.Description == i.Description);
                    
                    // Update the Item
                    oldItem = ServiceHelper<InventoryItem>.NoNullsAccepted(oldItem);
                    oldItem.Quantity += ci.Quantity;

                    //Save changes
                    await _repo.SaveChangesAsync();

                    //Return the DTO to the response handler
                    return new GetItemDTO(oldItem);
                }

                // Case: the item is completely new in this inventory. Create Item object
                InventoryItem nw = new()
                {
                    Id = Guid.NewGuid(),
                    Name = ci.Name,
                    Description = ci.Description,
                    Quantity = ci.Quantity,
                    UserId = ci.UserId,
                };

                // The Owner of the new Item is the target User
                nw.Owner = targetUser;

                // Apply changes to the user Inventory
                await _repo.Items.AddAsync(nw);
                await _repo.SaveChangesAsync();

                // Return DTO to the Response Handler
                return new GetItemDTO(nw);
            }

            return await ServiceHelper<GetItemDTO>.ActionHandler(rawCreation, currenItem);
        }

        public async Task<ServiceResponse<GetItemDTO>> DeleteItem(Guid id)
        {
            async Task<GetItemDTO> rawElimination(object obj)
            {
                // Get the ID of the item to delete
                var idn = (Guid)obj;

                // DFind the Item in the Database
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

        public async Task<List<InventoryItem>> GetItemsOfUser(int userId)
        {
            List<InventoryItem> userInv = new();
            try
            {
                var targetUser = await this._uService.GetUserRaw(userId);
                userInv = ServiceHelper<List<InventoryItem>>.NoNullsAccepted(targetUser.Items);
            }
            catch (Exception err)
            {
                throw new NullReferenceException($"Error: {err.Message}");
            }
            return userInv;
        }

        private bool IsItemInList(List<InventoryItem> list, string name, string description)
        {
            return list.Exists(i => i.Name == name && i.Description == description);
        }
    }
}