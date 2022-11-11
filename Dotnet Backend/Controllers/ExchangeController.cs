using DotnetBackend.DTOs;
using DotnetBackend.Models;
using DotnetBackend.Services.ExchangeServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeController : Controller
    {
        public IExchangeService _eServ;

        public ExchangeController(IExchangeService _eServ)
        {
            this._eServ = _eServ;
        }
        #region Endpoints
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<GetExchangeDTO>>> GetAllExchanges()
        {
            var result = await _eServ.GetAllExchanges();
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetExchangeDTO>>> GetExchange(Guid id)
        {
            var result = await _eServ.GetExchange(id);
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        [HttpPost()]
        public async Task<ActionResult<ServiceResponse<GetExchangeDTO>>> EnactExchange(CreateExchangeDTO nwEx)
        {
            var result = await _eServ.EnactExchange(nwEx);
            return result.Successfull ? Ok(result) : NotFound(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetItemDTO>>> RevertExchange(Guid id)
        {
            var result = await _eServ.RevertExchange(id);
            return result.Successfull ? Ok(result) : NotFound(result);
        }

        #endregion
    
    }
}