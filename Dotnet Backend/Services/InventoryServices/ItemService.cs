using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;
using users_items_backend.Context;
using DotnetBackend.DTOs;
using DotnetBackend.Services.UserServices;

namespace DotnetBackend.Services.Inventory
{
    /// <summary>
    /// Service for the InventoryItems
    /// </summary>
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
        /// <summary>
        /// Adds an Item to the DB
        /// </summary>
        /// <param name="currenItem">The DTO that encapsulates the data recieved from the Frontend</param>
        /// <returns>An async ServiceResponse stating weather or not the creation was successfull</returns>
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

        /// <summary>
        /// Deletes an Item from the DB
        /// </summary>
        /// <param name="id">The GUID identifying the Item to delete</param>
        /// <returns>An async ServiceResponse stating weather or not the deletion was successfull</returns>
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

        /// <summary>
        /// Returns a single Item from the DB
        /// </summary>
        /// <param name="id">The GUID identifying the Item to get</param>
        /// <returns>An async ServiceResponse stating weather or not the request was successfull</returns>
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

        /// <summary>
        /// Requests the DB to display all InventoryItems
        /// </summary>
        /// <returns>An async ServiceResponse stating weather or not the request was successfull</returns>
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

        /// <summary>
        /// Updates an InventoryItem in the DB
        /// </summary>
        /// <param name="currenItem">A Data Transfer Object with the new information but the original GUID</param>
        /// <returns>An async ServiceResponse stating weather or not the changes in DB were successfull</returns>
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
                old.UserId = ci.userId;

                await _repo.SaveChangesAsync();
                return new GetItemDTO(old);
            }
            return await ServiceHelper<GetItemDTO>.ActionHandler(rawUpdate, currenItem);
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// Requests the Items in the User's Inventory
        /// </summary>
        /// <param name="userId">The ID of the User of which the Inventory is requested</param>
        /// <returns>An async List with the Items in the User's Inventory</returns>
        /// <exception cref="NullReferenceException">When the UserID doesn't exists in the DB</exception>
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

        /// <summary>
        /// Checks if the given List contains an item with the same name and description
        /// </summary>
        /// <param name="list">The inventory of items to check</param>
        /// <param name="name">The name of the Item to find</param>
        /// <param name="description">The description to match with the name of the Item</param>
        /// <returns>True if the Item is in the Inventory, false if it's not</returns>
        public bool IsItemInList(List<InventoryItem> list, string name, string description)
        {
            return list.Exists(i => i.Name == name && i.Description == description);
        }

        /// <summary>
        /// Auxiliary method to find am existing item owned by a specific user
        /// </summary>
        /// <param name="userid">The ID of the owner User</param>
        /// <param name="name">The name of the Item to find</param>
        /// <param name="description">The description of the Item to find</param>
        /// <returns>The Item object that matches the given parameters</returns>
        public async Task<InventoryItem> GetExistingItemRaw(int userid, string name, string description)
        {
            var reItem = await _repo.Items
                .Include(i=> i.Owner)
                .FirstOrDefaultAsync(
                    i=>i.Owner.Id == userid && i.Name == name && i.Description == description);
            return ServiceHelper<InventoryItem>.NoNullsAccepted(reItem);
        }
        #endregion
    }
}