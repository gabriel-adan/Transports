using Business.Layer.Model;
using System.Collections.Generic;

namespace Logic.Layer.Contracts
{
    public interface IDriverLogic
    {
        Driver Create(string name);

        bool Save(Driver driver);

        bool Modify(Driver driver);

        IList<Driver> ToList();

        IList<Driver> ToList(int dayOfWeek);
    }
}
