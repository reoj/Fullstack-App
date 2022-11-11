using DotnetBackend.DTOs;
using DotnetBackend.Models;
using DotnetBackend.Services.Inventory;
using DotnetBackend.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using users_items_backend.Context;
using users_items_backend.DTOs;

namespace DotnetBackend.Services.ExchangeServices
{
    public class ExchangeService : IExchangeService
    {
        private readonly DataContext _repo;
        private readonly IInventoryService _iserv;
        private readonly IUserService _userv;

        public ExchangeService(DataContext context, IInventoryService iserv, IUserService userv)
        {
            this._userv = userv;
            this._iserv = iserv;
            _repo = context;
        }
        public async Task<ServiceResponse<GetExchangeDTO>> EnactExchange(CreateExchangeDTO toCreate)
        {
            var sr = new ServiceResponse<GetExchangeDTO>();
            try
            {
                sr.Body = await InventoryExchangeHandler(toCreate);
            }
            catch (Exception err)
            {

                throw new ExchangeException(err.Message);
            }
            return sr;
            // Func<CreateExchangeDTO, bool, Task<GetExchangeDTO>> invExchangeHandler = this.InventoryExchangeHandler;
            // return await ServiceHelper<GetExchangeDTO>.ActionHandler(invExchangeHandler,(object)toCreate,false);
        }

        public async Task<ServiceResponse<List<GetExchangeDTO>>> GetAllExchanges()
        {
            async Task<List<GetExchangeDTO>> rawGet(object a)
            {
                var exchanges = await _repo.Exchanges.ToListAsync();
                var dtoExchanges = new List<GetExchangeDTO>();
                exchanges.ForEach(e => dtoExchanges.Add(new GetExchangeDTO(e)));
                return dtoExchanges;
            };
            return await ServiceHelper<List<GetExchangeDTO>>.ActionHandler(rawGet, 0);
        }

        public async Task<ServiceResponse<GetExchangeDTO>> GetExchange(Guid requested)
        {
            async Task<GetExchangeDTO> rawGetter(object obj)
            {
                var idn = (Guid)obj;

                //Find the Exchange object based on the GUID
                var requested = await _repo.Exchanges.FirstOrDefaultAsync(u => u.Id == idn);
                return new GetExchangeDTO(ServiceHelper<Exchange>.NoNullsAccepted(requested));
            }

            return await ServiceHelper<GetExchangeDTO>.ActionHandler(rawGetter, requested);
        }

        public async Task<ServiceResponse<GetExchangeDTO>> RevertExchange(Guid toDelete)
        {
            var sr = new ServiceResponse<GetExchangeDTO>();
            try
            {
                var toDeleteDTO = await GetDTOFromExchangeID(toDelete);
                sr.Body = await InventoryExchangeHandler(toDeleteDTO, reverse: true);
            }
            catch (Exception err)
            {

                throw new ExchangeException(err.Message);
            }
            return sr;
        }

        #region Auxiliary
        private async Task<(User sender, User reciever)> VerifyUsersById(int idSender, int idReciever)
        {
            return (
                sender: await AwaitUserRequest(idSender), reciever: await AwaitUserRequest(idReciever)
            );
        }

        public async Task<GetExchangeDTO> InventoryExchangeHandler
            (CreateExchangeDTO ce, bool reverse = false)
        {
            // Check if the Users Exist
            // Get the Sender and Reciever Users from Data context
            (var cSender, var cReciever) = reverse == false ? await VerifyUsersById(ce.Sender, ce.Reciever) : await VerifyUsersById(ce.Reciever, ce.Sender);

            //Request the sender's Inventory
            var sendersInv = await _iserv.GetItemsOfUser(ce.Sender);

            var exchangeItem = await _iserv.GetExistingItemRaw(
                    cSender.Id, ce.ItemName, ce.ItemDescription);

            // Check if the item is in the inventory of the Sender
            if (_iserv.IsItemInList(sendersInv, ce.ItemName, ce.ItemDescription)
                && exchangeItem.Quantity >= ce.ItemQuantity)
            {
                Exchange nw = new()
                {
                    Id = Guid.NewGuid(),
                    itemName = ce.ItemName,
                    itemDescription = ce.ItemDescription,
                    itemQuantity = ce.ItemQuantity,
                };

                //Enact changes
                // 1. Add Items to reciever's inventory
                var responseFromAdding = await _iserv.CreateItem(
                    new CreateIttemDTO()
                    {
                        UserId = cReciever.Id,
                        Name = ce.ItemName,
                        Description = ce.ItemDescription,
                        Quantity = ce.ItemQuantity
                    }
                );

                //2. Remove Items from sender's inventory
                exchangeItem.Quantity -= ce.ItemQuantity;
                exchangeItem.UserId = cSender.Id;
                var responseFromUpdating = await _iserv.UpdateItem(Mappings.AsUpdateItemDTO(exchangeItem));

                if (responseFromAdding.Successfull && responseFromUpdating.Successfull)
                {
                    // Save record
                    await _repo.Exchanges.AddAsync(nw);
                    await _repo.SaveChangesAsync();

                    // Return created object to Service Response
                    return new GetExchangeDTO(nw);
                }
                else
                {
                    string msj = "The Requests couldn't be compleated:";
                    msj += $"On Adding:{responseFromAdding.Message}";
                    msj += $"\nOn Updating{responseFromUpdating.Message}";
                    throw new ExchangeException(msj);
                }
            }
            else
            {
                throw new ExchangeException(
                    $"The Item requested is not in the inventory of user with ID: {ce.Sender}");
            }

        }

        private async Task<User> AwaitUserRequest(int userId)
        {
            return ServiceHelper<User>.NoNullsAccepted(await _userv.GetUserRaw(userId));
        }


        private async Task<CreateExchangeDTO> GetDTOFromExchangeID(Guid idn)
        {
            var requested = await _repo.Exchanges.FirstOrDefaultAsync(u => u.Id == idn);
            return new CreateExchangeDTO(ServiceHelper<Exchange>.NoNullsAccepted(requested));
        }
        private class ExchangeException : Exception
        {
            public ExchangeException(string Message) : base(message: Message) { }
        }
        #endregion
    }
}