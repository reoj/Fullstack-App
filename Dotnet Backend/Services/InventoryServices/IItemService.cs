using exam_webapi.DTOs.ItemDTOs;
using exam_webapi.Models;
using exam_webapi.Repositories;
using exam_webapi.Services.UserServices;

namespace exam_webapi.Services.Inventory
{
    public class ItemService:IInventoryService
    {
        public List<InventoryItem> _repo { get; set; }

        public ItemService()
        {
            _repo = StaticData.InventoryContext;
        }
        public InventoryItem CreateItem(CreateIttemDTO currenItem)
        {
            InventoryItem nw = new(){
                Id = Guid.NewGuid(),
                Name = currenItem.Name,
                Description = currenItem.Description,
                Quantity = currenItem.Quantity,
                UserId = currenItem.UserId,
            };
            _repo.Add(nw);
            return nw;
        }

        public InventoryItem DeleteItem(Guid id)
        {
            var old = _repo.Where(u => u.Id == id).SingleOrDefault();
            _repo.Remove(old);
            return old;
        }

        public InventoryItem GetItem(Guid id)
        {
            return _repo.Where(u => u.Id == id).SingleOrDefault();
        }

        public InventoryItem UpdateItem(UpdateItemDTO currenItem)
        {
            var old = _repo.Where(u => u.Id == currenItem.ItemId).SingleOrDefault();
            int index = _repo.IndexOf(old);
            InventoryItem nw = new(){
                Id = old.Id,
                Name = currenItem.Name,
                Description = currenItem.Description,
                Quantity = currenItem.Quantity,
                UserId = currenItem.UserId,
            };
            _repo[index] = nw;
            return nw;
        }
    }
}