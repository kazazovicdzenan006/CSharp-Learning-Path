using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IRepository<T> where T : class {
        public Task<IEnumerable<T>> GetAll();
        public Task Add(T entity);
        public Task<T> FindById(int id); 

        public void Delete(T entity);
        public void Update(T entity);

        public IQueryable<T> GetQueryable();

    
    
    }

}
