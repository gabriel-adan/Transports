using Business.Layer.Model;
using System.Collections.Generic;

namespace Business.Layer.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IList<Customer> ToList();
    }
}