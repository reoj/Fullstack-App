using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DotnetBackend.DTOs;
using DotnetBackend.Models;
using DotnetBackend.Services.ExchangeServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotnetBackend.Controllers
{
    [Route("[controller]")]
    public class ExchangeController : Controller
    {
        public ExchangeService _eServ;

        public ExchangeController(ExchangeService _eServ)
        {
            this._eServ = _eServ;
        }
        #region Endpoints
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<GetExchangeDTO>>> GetAllExchanges(Guid id)
        {
            var result = await _eServ.GetAllExchanges();
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetExchangeDTO>>> GetItem(Guid id)
        {
            var result = await _eServ.GetExchange(id);
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetExchangeDTO>>> CreateItem(CreateExchangeDTO nwEx)
        {
            var result = await _eServ.EnactExchange(nwEx);
            return result.Successfull ? Ok(result) : NotFound(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetItemDTO>>> DeleteItem(Guid id)
        {
            var result = await _eServ.RevertExchange(id);
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        #endregion
    
    }
}