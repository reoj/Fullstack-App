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
        public async Task<ActionResult<ServiceResponse<InventoryItem>>> CreateUser(CreateIttemDTO nwItem){
            var result = await _inventory.CreateItem(nwItem);
            return result.Successfull ? Ok(result) : NotFound(result);
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<InventoryItem>>> GetUser(Guid id){
            var result = await _inventory.GetItem(id); 
            return result.Successfull ? Ok(result) : NotFound(result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<InventoryItem>>> UpdateUser(UpdateItemDTO nwItem){
            var result = await _inventory.UpdateItem(nwItem); 
            return result.Successfull ? Ok(result) : NotFound(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<InventoryItem>>> DeleteUser(Guid id){
            var result = await _inventory.DeleteItem(id);
            return result.Successfull ? Ok(result) : NotFound(result);
        }

    }
}