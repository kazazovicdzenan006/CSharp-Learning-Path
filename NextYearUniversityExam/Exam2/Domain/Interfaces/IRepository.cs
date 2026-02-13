using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
  public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>>  GetAllData();
        Task<T> FindById(int id);
        Task AddObject(T obj);
        public void UpdateObject(T obj); 
        public void DeleteObject(T obj);

        IQueryable<T> GetQueryable();

    }
}
