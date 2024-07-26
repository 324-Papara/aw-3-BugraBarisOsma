using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command;

public class CustomerCommandHandler :
    IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>,
    IRequestHandler<UpdateCustomerCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ILogger<CustomerCommandHandler> logger;

    public CustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CustomerCommandHandler> logger)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
       
        
        var mapped = mapper.Map<CustomerRequest, Customer>(request.Request);
        mapped.CustomerNumber = new Random().Next(1000000, 9999999);
        await unitOfWork.CustomerRepository.Insert(mapped);
        await unitOfWork.Complete();
        var response = mapper.Map<CustomerResponse>(mapped);
        
     
        return new ApiResponse<CustomerResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
  
        
        var mapped = mapper.Map<CustomerRequest, Customer>(request.Request);
        mapped.Id = request.CustomerId;
        unitOfWork.CustomerRepository.Update(mapped);
        await unitOfWork.Complete();
        
       
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        
      
        
        await unitOfWork.CustomerRepository.Delete(request.Id);
        await unitOfWork.Complete();
        
    
        return new ApiResponse();
    }
}