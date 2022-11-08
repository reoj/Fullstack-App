using DotnetBackend.DTOs;
using DotnetBackend.Models;

namespace DotnetBackend.Services.ExchangeServices
{
    public interface IExchangeService
    {
        Task<ServiceResponse<GetExchangeDTO>> EnactExchange(CreateExchangeDTO toCreate);
        Task<ServiceResponse<GetExchangeDTO>> GetExchange(Guid requested);
        Task<ServiceResponse<List<GetExchangeDTO>>> GetAllExchanges();
        Task<ServiceResponse<GetExchangeDTO>> RevertExchange(Guid toDelete);
    }
}