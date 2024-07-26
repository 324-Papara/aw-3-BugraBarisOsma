using Para.Base.Response;
using Para.Data.Domain;

namespace Para.Data.DapperRepository;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetCustomerDetailsAsync();
}