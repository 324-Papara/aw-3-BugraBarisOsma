using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPhonesController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public CustomerPhonesController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Get()
        {
            var operation = new GetAllCustomerPhoneQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerPhoneResponse>> Get([FromRoute]long customerId)
        {
            var operation = new GetCustomerPhoneByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerPhoneResponse>> Post([FromBody] CustomerPhoneRequest value)
        {
            var operation = new CreateCustomerPhoneCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerPhoneRequest value)
        {
            var operation = new UpdateCustomerPhoneCommand(customerId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{Id}")]
        public async Task<ApiResponse> Delete(long Id)
        {
            var operation = new DeleteCustomerPhoneCommand(Id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
