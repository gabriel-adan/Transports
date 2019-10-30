using Business.Layer.Contracts;
using Business.Layer.Model;
using Logic.Layer.Contracts;
using System.Collections.Generic;

namespace Logic.Layer.Implementation
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IHourRepository hourRepository;

        public CustomerLogic(ICustomerRepository customerRepository, IHourRepository hourRepository)
        {
            this.customerRepository = customerRepository;
            this.hourRepository = hourRepository;
        }

        public Customer Create(string name, string address)
        {
            return new Customer() { Name = name, Address = address };
        }

        public IList<Customer> ToList()
        {
            try
            {
                return customerRepository.ToList();
            }
            catch
            {
                throw;
            }
        }

        public bool Save(Customer customer)
        {
            try
            {
                return customerRepository.Save(customer);
            }
            catch
            {
                throw;
            }
        }

        public bool Modify(Customer customer)
        {
            try
            {
                return customerRepository.Update(customer);
            }
            catch
            {
                throw;
            }
        }

        public bool Remove(Customer customer)
        {
            try
            {
                return customerRepository.Delete(customer);
            }
            catch
            {
                throw;
            }
        }
    }
}