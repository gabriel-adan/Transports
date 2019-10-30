using Business.Layer.Model;
using Business.Layer.Contracts;
using System.Data.ORM.Context;
using System.Data.ORM.Queries;
using System.Collections.Generic;

namespace Infraestructure.Layer.Repository
{
    public class HourRepository : GenericRepository<Hour>, IHourRepository
    {
        public HourRepository(DbContext context) : base(context)
        {

        }

        public IList<Hour> ListByDayOfWeek(int dayOfWeek)
        {
            try
            {
                return dbSet.Where(h => h.DayOfWeek == dayOfWeek).OrderBy(h => h.EntryTime, Order.ASC);
            }
            catch
            {
                throw;
            }
        }

        public bool Update(Hour hour, Hour modified)
        {
            try
            {
                return dbSet.Update(hour, modified);
            }
            catch
            {
                throw;
            }
        }
    }
}
