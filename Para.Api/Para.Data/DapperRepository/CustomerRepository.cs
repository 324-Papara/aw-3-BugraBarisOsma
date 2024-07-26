using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Para.Data.Domain;

namespace Para.Data.DapperRepository;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbConnection _dbConnection;

    public CustomerRepository(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("PostgresSqlConnection"));
    }

    public async Task<IEnumerable<Customer>> GetCustomerDetailsAsync()
    {
        var sql = @"
            SELECT c.Id AS CustomerId, c.Name, 
                   ca.Address, 
                   cd.Detail, 
                   cp.PhoneNumber
            FROM Customers c
            LEFT JOIN CustomerAddresses ca ON c.Id = ca.CustomerId
            LEFT JOIN CustomerDetails cd ON c.Id = cd.CustomerId
            LEFT JOIN CustomerPhones cp ON c.Id = cp.CustomerId
            WHERE c.Id = @CustomerId;
        ";

        var customers = await _dbConnection.QueryAsync<Customer, CustomerAddress, CustomerDetail, CustomerPhone, Customer>(
            sql,
            (customer, address, detail, phone) =>
            {
                
                customer.CustomerAddresses = new List<CustomerAddress>();
                customer.CustomerDetail = detail;
                customer.CustomerPhones = new List<CustomerPhone>();
                return customer;
            },
            
            splitOn: "Address,Detail,PhoneNumber"
        );

        return customers.ToList();
    }
}
