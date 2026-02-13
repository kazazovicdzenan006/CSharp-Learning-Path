using System;
using System.Collections.Generic;
using System.Text;
using Data.Context;
using Data.Repositories;
using Domain.Interfaces;
using Domain.Models;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MasterContext _context;
        public UnitOfWork(MasterContext context)
        {
            _context = context;
            people = new Repository<Person>(_context);
            worker = new Repository<Worker>(_context);
            client = new Repository<Client>(_context);
        }


        public IRepository<Person> people { get; private set; }
        public IRepository<Worker> worker { get; private set; }
        public IRepository<Client> client { get; private set; }
        public async Task CompleteSave()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose() => _context.Dispose();


    }
}
