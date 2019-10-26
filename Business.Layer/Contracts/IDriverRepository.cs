using Business.Layer.Model;
using System.Collections.Generic;

namespace Business.Layer.Contracts
{
    public interface IDriverRepository : IRepository<Driver>
    {
        IList<Driver> ToList();

        IList<Driver> ToList(int dayOfWeek);
    }
}