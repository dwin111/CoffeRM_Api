namespace CoffeCRMBeck.DAL.@interface
{
    public interface IRepository<T>
    {

        Task<bool> Create(T model);
        Task<bool> Edit(T model);
        IQueryable<T> GetAll();

    }
}

