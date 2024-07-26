using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Data.DapperRepository;
using Para.Data.Domain;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReportController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerReportController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            var result = await _customerRepository.GetCustomerDetailsAsync();
            return result;
        }
    }
}
