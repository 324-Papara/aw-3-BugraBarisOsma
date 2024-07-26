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
    public class CustomerDetailsController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public CustomerDetailsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Get()
        {
            var operation = new GetAllCustomerDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> Get([FromRoute]long customerId)
        {
            var operation = new GetCustomerDetailByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerDetailResponse>> Post([FromBody] CustomerDetailRequest value)
        {
            var operation = new CreateCustomerDetailCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerDetailRequest value)
        {
            var operation = new UpdateCustomerDetailCommand(customerId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{Id}")]
        public async Task<ApiResponse> Delete(long Id)
        {
            var operation = new DeleteCustomerDetailCommand(Id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
