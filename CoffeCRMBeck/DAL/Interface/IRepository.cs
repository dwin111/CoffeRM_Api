namespace CoffeCRMBeck.DAL.@interface
{
    public interface IRepository<T>
    {

        Task<bool> CreateAsync(T model);
        Task<bool> EditAsync(T model);
        IQueryable<T> GetAll();

    }
}

