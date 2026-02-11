using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Data.Interfaces
{
    public interface IUnitOfWork : IDisposable { 
        public IRepository<Project> Projects { get; }
        public IRepository<Person> People { get;  }

        public IRepository<TeamMember> Member { get; }

        public Task CompleteSave(); 

    
    
    }

}
