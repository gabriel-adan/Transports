using Business.Layer.Contracts;
using Business.Layer.Model;
using Logic.Layer.Contracts;
using System.Collections.Generic;

namespace Logic.Layer.Implementation
{
    public class DriverLogic : IDriverLogic
    {
        private readonly IDriverRepository driverRepository;

        public DriverLogic(IDriverRepository driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        public Driver Create(string name)
        {
            return new Driver() { Name = name };
        }

        public bool Save(Driver driver)
        {
            try
            {
                return driverRepository.Save(driver);
            }
            catch
            {
                throw;
            }
        }

        public bool Modify(Driver driver)
        {
            try
            {
                return driverRepository.Update(driver);
            }
            catch
            {
                throw;
            }
        }

        public bool Remove(Driver driver)
        {
            try
            {
                return driverRepository.Delete(driver);
            }
            catch
            {
                throw;
            }
        }

        public IList<Driver> ToList()
        {
            try
            {
                return driverRepository.ToList();
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
                return driverRepository.ToList(dayOfWeek);
            }
            catch
            {
                throw;
            }
        }
    }
}
