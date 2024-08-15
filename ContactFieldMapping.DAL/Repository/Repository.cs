using ContactFieldMapping.DAL.Data;
using ContactFieldMapping.DAL.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ContactFieldMapping.DAL.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
