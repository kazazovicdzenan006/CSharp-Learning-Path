using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Services.Services
{
    public class ExamService
    {
        private readonly IUnitOfWork _unit;
        private  IQueryable<Worker> Workers => _unit.people.GetQueryable().OfType<Worker>();
        public ExamService() { }
        public ExamService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<IEnumerable<Person>> GetData()
        {
            return await _unit.people.GetAllData();
        }
        public async Task<IEnumerable<Client>> GetClients()
        {
            return await _unit.client.GetAllData();
        }

        public async Task<IEnumerable<Worker>> GetWorkers()
        {
            return await _unit.worker.GetAllData();
        }
        public async Task<Client> GetClientById(int id)
        {
            return await _unit.client.FindById(id);
        }

        public async Task<Worker> GetWorkerById(int id)
        {
            return await _unit.worker.FindById(id);
        }

        public async Task AddClient(Client obj)
        {
       

            await _unit.client.AddObject(obj);
            await _unit.CompleteSave();
        }
        public async Task AddWorker(Worker obj)
        {
         
            await _unit.worker.AddObject(obj);
            await _unit.CompleteSave();
        }
        public async Task RemoveClient(Client obj)
        {
            if (await _unit.client.FindById(obj.Id) == null)
            {
                throw new Exception("That client doesn't exist");
            }
            _unit.client.DeleteObject(obj);
            await _unit.CompleteSave();
        }

        public async Task RemoveWorker(Worker obj)
        {
            if (await _unit.worker.FindById(obj.Id) == null)
            {
                throw new Exception("That worker doesn't exist");
            }
            _unit.worker.DeleteObject(obj);
            await _unit.CompleteSave();
        }

        public async Task UpdateWorker(Worker obj)
        {
            var exist = await _unit.worker.FindById(obj.Id); 
            if (exist != null)
            {
                exist.Name = obj.Name;
                exist.Position = obj.Position;
                exist.ExperienceInYears = obj.ExperienceInYears;
            }else
            {
                throw new Exception("That worker doesn't exist");
            }
            await _unit.CompleteSave();
        }

        public async Task UpdateClient(Client obj)
        {
            var exist = await _unit.client.FindById(obj.Id);
            if (exist != null)
            {
                exist.Name = obj.Name;
                exist.CompanyName = obj.CompanyName;
                exist.Email = obj.Email;
                
            }
            else
            {
                throw new Exception("That Client doesn't exist");
            }
            await _unit.CompleteSave();
        }


        public async Task<object> GroupedWorkers()
        {

            return await Workers.GroupBy(x => x.Position)
                .Select(x => new { Position = x.Key, Employees = x.ToList() })
               .ToListAsync();

        } 

        public async Task<Worker?> MostExperiencedWorker()
        {
            
            return await Workers.OrderByDescending(x => x.ExperienceInYears).FirstOrDefaultAsync();

        }








    }
}
