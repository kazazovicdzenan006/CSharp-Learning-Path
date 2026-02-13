using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable 
    {
        public IRepository<Person> people { get; }
        public IRepository<Worker> worker { get; }

        public IRepository<Client> client { get; }

        Task CompleteSave();

    }
}
