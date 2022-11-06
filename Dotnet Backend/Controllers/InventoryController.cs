using DotnetBackend.Models;
using exam_webapi.DTOs.ItemDTOs;
using exam_webapi.Models;
using exam_webapi.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace exam_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController:ControllerBase
    {
        private readonly IInventoryService _inventory;
        
        public InventoryController(IInventoryService inventory)
        {
            this._inventory = inventory;            
        }
        
        [HttpPost]
        public ActionResult<ServiceResponse<InventoryItem>> CreateUser(CreateIttemDTO nwItem){
            return Ok(_inventory.CreateItem(nwItem));
            
        }
        [HttpGet("{id}")]
        public ActionResult<ServiceResponse<InventoryItem>> GetUser(Guid id){
            return Ok(_inventory.GetItem(id));
        }
        
        [HttpPut("{id}")]
        public ActionResult<ServiceResponse<InventoryItem>> UpdateUser(UpdateItemDTO nwItem){
            return Ok(_inventory.UpdateItem(nwItem));
        }
        [HttpDelete("{id}")]
        public ActionResult<ServiceResponse<InventoryItem>> DeleteUser(Guid id){
            return Ok(_inventory.DeleteItem(id));
        }

    }
}