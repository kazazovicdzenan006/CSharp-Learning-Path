using System;
using System.Collections.Generic;
using System.Text;
using Data.Context;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Data.Repositories
{
    
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MasterContext _context; 
        private readonly DbSet<T> _dbSet;
        public Repository(MasterContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public Repository(MasterContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task<IEnumerable<T>> GetAllData() => await _dbSet.ToListAsync();
        public async Task<T> FindById(int id) => await _dbSet.FindAsync(id);
        public async Task AddObject(T obj) => await _dbSet.AddAsync(obj);

        public void UpdateObject(T obj) => _dbSet.Update(obj);
        public void DeleteObject(T obj) => _dbSet.Remove(obj);
        public IQueryable<T> GetQueryable()
        {
            return  _dbSet.AsQueryable();
        }



    }
    
}
