using Business.Layer.Model;
using Business.Layer.Contracts;
using System.Data.ORM.Context;
using System.Collections.Generic;

namespace Infraestructure.Layer.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {

        }

        public IList<Customer> ToList()
        {
            try
            {
                return dbSet.OrderBy(c => c.Name);
            }
            catch
            {
                throw;
            }
        }
    }
}
