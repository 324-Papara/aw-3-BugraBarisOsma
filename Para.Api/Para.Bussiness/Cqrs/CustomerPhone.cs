using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;



    public record CreateCustomerPhoneCommand(CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;
    public record UpdateCustomerPhoneCommand(long CustomerId,CustomerPhoneRequest Request) : IRequest<ApiResponse>;
    public record DeleteCustomerPhoneCommand(long Id) : IRequest<ApiResponse>;

    public record GetAllCustomerPhoneQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
    public record GetCustomerPhoneByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerPhoneResponse>>;
    public record GetCustomerPhoneByParametersQuery(long CustomerId,string Name,string IdentityNumber) : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
