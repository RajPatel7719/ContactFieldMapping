namespace ContactFieldMapping.DAL.IRepository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task SaveChangesAsync();
}
