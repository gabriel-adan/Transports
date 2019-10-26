using System;
using System.Collections.Generic;
using Business.Layer.Model;

namespace Logic.Layer.Contracts
{
    public interface IHourLogic
    {
        Hour Create();

        Hour Create(string place, TimeSpan entryTime, TimeSpan exitTime, int dayOfWeek, Customer customer, Driver entryDriver, Driver exitDriver);

        bool Save(Hour hour);

        bool Modify(Hour hour, Hour modified);

        bool Modify(Hour hour);

        bool Remove(Hour hour);

        IList<Hour> ListByDayOfWeek(int dayOfWeek);
    }
}
