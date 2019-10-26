using Business.Layer.Model;
using System.Collections.Generic;

namespace Business.Layer.Contracts
{
    public interface IHourRepository : IRepository<Hour>
    {
        bool Update(Hour hour, Hour modified);

        IList<Hour> ListByDayOfWeek(int dayOfWeek);
    }
}