namespace NorthwindMvc.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAssync();
    Task<T> GetByIdAssync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}


