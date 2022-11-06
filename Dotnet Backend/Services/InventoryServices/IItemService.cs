using Dotnet.Backend.Models;
using exam_webapi.DTOs.ItemDTOs;
using exam_webapi.Models;
using exam_webapi.Repositories;
using exam_webapi.Services.UserServices;

namespace exam_webapi.Services.Inventory
{
    public class ItemService : IInventoryService
    {
        public List<InventoryItem> _repo { get; set; }

        public ItemService()
        {
            _repo = StaticData.InventoryContext;
        }
        public ServiceResponse<InventoryItem> CreateItem(CreateIttemDTO currenItem)
        {

            Func<Object, InventoryItem> rawCreation = (obj) =>
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
                    _repo.Add(nw);
                    return nw;
                };

            return ActionHandler(rawCreation, currenItem);
        }

        public ServiceResponse<InventoryItem> DeleteItem(Guid id)
        {
            Func<Object, InventoryItem> rawElimination = (obj) =>
            {
                var idn = (Guid)obj;
                var old = _repo.Where(u => u.Id == idn).SingleOrDefault();
                old = NoNullsAccepted(old);
                _repo.Remove(old);
                return old;
            };
            return ActionHandler(rawElimination, id);
        }

        public ServiceResponse<InventoryItem> GetItem(Guid id)
        {
            Func<Object, InventoryItem> rawGetter = (obj) =>
            {
                var idn = (Guid)obj;
                var requested = _repo.Where(u => u.Id == id).SingleOrDefault();
                return NoNullsAccepted(requested);
            };

            return ActionHandler(rawGetter, id);
        }

        public ServiceResponse<InventoryItem> UpdateItem(UpdateItemDTO currenItem)
        {
            Func<Object, InventoryItem> rawUpdate = (obj) =>
            {
                var old = _repo.Where(u => u.Id == currenItem.ItemId).SingleOrDefault();
                old = NoNullsAccepted(old);
                int index = _repo.IndexOf(old);
                InventoryItem nw = new()
                {
                    Id = old.Id,
                    Name = currenItem.Name,
                    Description = currenItem.Description,
                    Quantity = currenItem.Quantity,
                    UserId = currenItem.UserId,
                };
                return _repo[index] = nw;
            };
            return ActionHandler(rawUpdate, currenItem);
        }
        #region Helpers
        ServiceResponse<InventoryItem> ActionHandler(Func<Object, InventoryItem> fn, Object arg)
        {
            var response = new ServiceResponse<InventoryItem>();
            try
            {
                response.Body = fn(arg);
            }
            catch (Exception err)
            {
                response.Successfull = false;
                string relevant =
                    $"The request couldn't be completed because of the following error:{err.Message.ToString()}";
                response.Message = relevant;
            }
            return response;
        }
        InventoryItem NoNullsAccepted(InventoryItem? toCheck)
        {
            if (toCheck is null)
            {
                throw new NullReferenceException($"No object could be found with that information");
            }
            return toCheck;
        }
        
        #endregion
    }
}