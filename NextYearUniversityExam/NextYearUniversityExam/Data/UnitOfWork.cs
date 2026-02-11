using Data.Interfaces;
using Data.Repository;
using Domain; 
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MasterContext _context;  
        public UnitOfWork(MasterContext context) {
            _context = context;
            People = new GenericRepository<Person>(context);
            Member = new GenericRepository<TeamMember>(context);
            Projects = new GenericRepository<Project>(context);
        
        }

        public IRepository<Person> People { get; private set; }
        public IRepository<TeamMember> Member { get; private set; }
        public IRepository<Project> Projects { get; private set; }


        public async Task CompleteSave() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();

    }
}
