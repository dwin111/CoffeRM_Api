namespace CoffeCRMBeck.DAL.@interface
{
    public interface IRepository<T>
    {

        Task<T> GetByIdAsync(long id);
        Task<bool> CreateAsync(T model);
        Task<bool> EditAsync(T model);
        Task<bool> DeleteAsync(T model);
        IQueryable<T> GetAll();

    }
}

