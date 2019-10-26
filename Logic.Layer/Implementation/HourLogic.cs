using System;
using Business.Layer.Contracts;
using Business.Layer.Model;
using Logic.Layer.Contracts;
using System.Collections.Generic;

namespace Logic.Layer.Implementation
{
    public class HourLogic : IHourLogic
    {
        private readonly IHourRepository hourRepository;

        public HourLogic(IHourRepository hourRepository)
        {
            this.hourRepository = hourRepository;
        }

        public Hour Create()
        {
            return new Hour();
        }

        public Hour Create(string place, TimeSpan entryTime, TimeSpan exitTime, int dayOfWeek, Customer customer, Driver entryDriver, Driver exitDriver)
        {
            try
            {
                if (string.IsNullOrEmpty(place))
                    throw new Exception("Debe especificar el lugar hacia donde se trasladará al cliente");
                return new Hour() { Place = place, EntryTime = entryTime, ExitTime = exitTime, DayOfWeek = dayOfWeek, Customer = customer, EntryDriver = entryDriver, ExitDriver = exitDriver, IsCanceled = false };
            }
            catch
            {
                throw;
            }
        }

        public bool Save(Hour hour)
        {
            try
            {
                return hourRepository.Save(hour);
            }
            catch
            {
                throw;
            }
        }

        public bool Modify(Hour hour, Hour modified)
        {
            try
            {
                return hourRepository.Update(hour, modified);
            }
            catch
            {
                throw;
            }
        }

        public bool Modify(Hour hour)
        {
            try
            {
                return hourRepository.Update(hour);
            }
            catch
            {
                throw;
            }
        }

        public bool Remove(Hour hour)
        {
            try
            {
                return hourRepository.Delete(hour);
            }
            catch
            {
                throw;
            }
        }

        public IList<Hour> ListByDayOfWeek(int dayOfWeek)
        {
            try
            {
                return hourRepository.ListByDayOfWeek(dayOfWeek);
            }
            catch
            {
                throw;
            }
        }
    }
}
