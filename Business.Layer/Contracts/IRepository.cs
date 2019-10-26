namespace Business.Layer.Contracts
{
    public interface IRepository<T> where T : class
    {
        T Find(object id);

        bool Save(T entity);

        bool Update(T entity);

        bool Delete(T entity);
    }
}
