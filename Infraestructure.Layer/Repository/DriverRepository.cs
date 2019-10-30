using Business.Layer.Model;
using Business.Layer.Contracts;
using System.Data.ORM.Context;
using System.Collections.Generic;

namespace Infraestructure.Layer.Repository
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(DbContext context) : base(context)
        {

        }

        public IList<Driver> ToList()
        {
            try
            {
                return dbSet.ToList();
            }
            catch
            {
                throw;
            }
        }

        public IList<Driver> ToList(int dayOfWeek)
        {
            try
            {
                IList<Driver> drivers = dbSet.ToList();
                var hourSet = context.Set<Hour>();
                foreach (Driver driver in drivers)
                {
                    long id = driver.Id;
                    var hours = hourSet.Where(h => h.DayOfWeek == dayOfWeek).ToList();
                    driver.Hours.Clear();
                    foreach (var hour in hours)
                    {
                        if ((hour.EntryDriver != null && hour.EntryDriver.Id == id) || (hour.ExitDriver != null && hour.ExitDriver.Id == id))
                            driver.Hours.Add(hour);
                    }
                }
                return drivers;
            }
            catch
            {
                throw;
            }
        }
    }
}
