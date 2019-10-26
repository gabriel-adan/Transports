using Business.Layer.Model;
using System.Collections.Generic;

namespace Logic.Layer.Contracts
{
    public interface ICustomerLogic
    {
        Customer Create(string name);

        IList<Customer> ToList();

        bool Save(Customer customer);

        bool Modify(Customer customer);

        bool Remove(Customer customer);
    }
}
