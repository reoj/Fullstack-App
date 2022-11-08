using DotnetBackend.DTOs;
using DotnetBackend.Models;
using DotnetBackend.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using users_items_backend.Context;

namespace DotnetBackend.Services.ExchangeServices
{
    public class ExchangeService : IExchangeService
    {
        private DataContext _repo;
        private IUserService userv;

        public ExchangeService(DataContext context, IUserService userserv)
        {
            _repo = context;
            userv = userserv;
        }
        public async Task<ServiceResponse<GetExchangeDTO>> EnactExchange(CreateExchangeDTO toCreate)
        {
            async Task<GetExchangeDTO> rawCreation(object obj)
            {
                // Create the new Exchange object from the old one
                var ce = (CreateExchangeDTO)obj;
                Exchange nw = new()
                {
                    Id = Guid.NewGuid(),
                    itemName = ce.itemName,
                    itemDescription = ce.itemDescription,
                    itemQuantity = ce.itemQuantity,
                };

                // Get the Sender and Reciever Users from Data context
                nw.sender = ServiceHelper<User>.NoNullsAccepted(
                    await _repo.Users
                        .FirstOrDefaultAsync(u => u.Id == ce.sender)
                );
                nw.reciever = ServiceHelper<User>.NoNullsAccepted(
                    await _repo.Users
                        .FirstOrDefaultAsync(u => u.Id == ce.reciever)
                );

                // Enact changes
                await _repo.Exchanges.AddAsync(nw);
                await _repo.SaveChangesAsync();

                // Return created object to Service Response
                return new GetExchangeDTO(nw);
            }
            return await ServiceHelper<GetExchangeDTO>.ActionHandler(rawCreation, toCreate);
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
                var requested = await _repo.Exchanges.FirstOrDefaultAsync(u => u.Id == idn);
                return new GetExchangeDTO(ServiceHelper<Exchange>.NoNullsAccepted(requested));
            }

            return await ServiceHelper<GetExchangeDTO>.ActionHandler(rawGetter, requested);
        }

        public async Task<ServiceResponse<GetExchangeDTO>> RevertExchange(Guid toDelete)
        {
            async Task<GetExchangeDTO> rawCreation(object obj)
            {
                // Request the Exchange object with the exchange data to remocve
                var oldId = (Guid)obj;
                var exToReomove = ServiceHelper<Exchange>.NoNullsAccepted(
                    await _repo.Exchanges.FirstOrDefaultAsync(e => e.Id == oldId)
                );

                // Get the Sender and Reciever Users from Data context
                var sender = ServiceHelper<User>.NoNullsAccepted(
                    await _repo.Users
                        .FirstOrDefaultAsync(u => u.Id == exToReomove.sender.Id)
                );
                var reciever = ServiceHelper<User>.NoNullsAccepted(
                    await _repo.Users
                        .FirstOrDefaultAsync(u => u.Id == exToReomove.reciever.Id)
                );

                // Enact changes
                await _repo.SaveChangesAsync();

                // Return created object to Service Response
                return new GetExchangeDTO(exToReomove);
            }
            return await ServiceHelper<GetExchangeDTO>.ActionHandler(rawCreation, toDelete);
        }

        private void InventoryExchangeHandler(Exchange toManage, bool reverse = false){
            var originUser = ServiceHelper<User>.NoNullsAccepted(toManage.sender);
            var destinUser = ServiceHelper<User>.NoNullsAccepted(toManage.reciever);
            if (reverse)
            {
                var temp = originUser;
                originUser = destinUser;
                destinUser = temp;
            }
            
            try
            {
                var orign = _repo.Items
                    .Include(i=>i.Owner)
                    .Where(i => i.Owner == originUser && i.Name==toManage.itemName && i.Description == toManage.itemDescription);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}