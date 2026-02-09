

using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly MasterContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(MasterContext context, DbSet<T> dbSet){
        _context = context;
        _dbSet = dbSet; 
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity); 


}




