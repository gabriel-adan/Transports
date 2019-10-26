using Business.Layer.Contracts;

namespace Infraestructure.Layer.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public GenericRepository()
        {

        }

        public T Find(object id)
        {
            try
            {
                throw new System.NotImplementedException();
            }
            catch
            {
                throw;
            }
        }

        public bool Save(T entity)
        {
            try
            {
                throw new System.NotImplementedException();
            }
            catch
            {
                throw;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                throw new System.NotImplementedException();
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                throw new System.NotImplementedException();
            }
            catch
            {
                throw;
            }
        }
    }
}
