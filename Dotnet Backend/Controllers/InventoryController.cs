using DotnetBackend.Models;
using exam_webapi.DTOs.ItemDTOs;
using exam_webapi.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using users_items_backend.DTOs.ItemDTOs;

namespace exam_webapi.Controllers
{
    /// <summary>
    /// Controller for Items
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventory;

        #region Constructor
        public InventoryController(IInventoryService inventory)
        {
            this._inventory = inventory;
        }
        #endregion

        #region Endpoints
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<GetItemDTO>>> GetAllItems(Guid id)
        {
            var result = await _inventory.GetAllItems();
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetItemDTO>>> GetItem(Guid id)
        {
            var result = await _inventory.GetItem(id);
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetItemDTO>>> CreateItem(CreateIttemDTO nwItem)
        {
            var result = await _inventory.CreateItem(nwItem);
            return result.Successfull ? Ok(result) : NotFound(result);

        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetItemDTO>>> UpdateItem(UpdateItemDTO nwItem)
        {
            var result = await _inventory.UpdateItem(nwItem);
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetItemDTO>>> DeleteItem(Guid id)
        {
            var result = await _inventory.DeleteItem(id);
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        #endregion
    }
}