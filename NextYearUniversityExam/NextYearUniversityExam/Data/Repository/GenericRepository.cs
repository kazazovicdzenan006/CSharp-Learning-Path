namespace Data.Repository;
   using Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly MasterContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(MasterContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public GenericRepository(MasterContext context, DbSet<T> dbSet) {
        _context = context;
        _dbSet = dbSet;
    }


    public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync(); 

    public async Task Add(T entity) => await _dbSet.AddAsync(entity);

    public async Task<T> FindById(int id) => await _dbSet.FindAsync(id);

    public void Delete(T entity) => _dbSet.Remove(entity); 

    public void Update(T entity) => _dbSet.Update(entity);

    public IQueryable<T> GetQueryable() { return _dbSet.AsQueryable(); }

    



}

