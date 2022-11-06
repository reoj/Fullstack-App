using DotnetBackend.Models;
using DotnetBackend.Services;
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

            return ServiceHelper<InventoryItem>.ActionHandler(rawCreation, currenItem);
        }

        public ServiceResponse<InventoryItem> DeleteItem(Guid id)
        {
            Func<Object, InventoryItem> rawElimination = (obj) =>
            {
                var idn = (Guid)obj;
                var old = _repo.Where(u => u.Id == idn).SingleOrDefault();
                old = ServiceHelper<InventoryItem>.NoNullsAccepted(old);
                _repo.Remove(old);
                return old;
            };
            return ServiceHelper<InventoryItem>.ActionHandler(rawElimination, id);
        }

        public ServiceResponse<InventoryItem> GetItem(Guid id)
        {
            Func<Object, InventoryItem> rawGetter = (obj) =>
            {
                var idn = (Guid)obj;
                var requested = _repo.Where(u => u.Id == idn).SingleOrDefault();
                return ServiceHelper<InventoryItem>.NoNullsAccepted(requested);
            };

            return ServiceHelper<InventoryItem>.ActionHandler(rawGetter, id);
        }
        public ServiceResponse<InventoryItem> UpdateItem(UpdateItemDTO currenItem)
        {
            Func<Object, InventoryItem> rawUpdate = (obj) =>
            {
                var ci = (UpdateItemDTO)obj;
                var old = _repo.Where(u => u.Id == ci.ItemId).SingleOrDefault();
                old = ServiceHelper<InventoryItem>.NoNullsAccepted(old);
                int index = _repo.IndexOf(old);
                InventoryItem nw = new()
                {
                    Id = old.Id,
                    Name = ci.Name,
                    Description = ci.Description,
                    Quantity = ci.Quantity,
                    UserId = ci.UserId,
                };
                return _repo[index] = nw;
            };
            return ServiceHelper<InventoryItem>.ActionHandler(rawUpdate, currenItem);
        }
    }
}