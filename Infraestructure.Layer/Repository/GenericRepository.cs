using Business.Layer.Contracts;
using System.Data.ORM.Context;
using System.Data.ORM.Queries;

namespace Infraestructure.Layer.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;
        protected readonly IDbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public T Find(object id)
        {
            try
            {
                return dbSet.Find(id);
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
                return dbSet.Save(entity);
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
                return dbSet.Update(entity);
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
                return dbSet.Delete(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
