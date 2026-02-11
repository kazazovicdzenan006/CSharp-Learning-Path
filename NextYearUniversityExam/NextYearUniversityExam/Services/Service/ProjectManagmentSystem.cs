using Data.Interfaces;
using Domain;
using Domain.Exceptions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Services.Service
{

    public class ProjectManagmentService
    {
        private readonly IUnitOfWork _unit;
        private IQueryable<TeamMember> MembersInTeam => _unit.People.GetQueryable().OfType<TeamMember>();
        private IQueryable<Project> projects => _unit.Projects.GetQueryable().OfType<Project>();
        public ProjectManagmentService(IUnitOfWork unit)
        {
            _unit = unit;
        }



        public DateTime DateParse(string input)
        {
            string[] availableDateFormats = { "dd.MM.yyyy", "dd-MM-yyyy", "yyyy-MM-dd", "yyyy.MM.dd", "MMMM yyyy", "dd/MM/yyyy", "yyyy/MM/dd" };
            bool check = DateTime.TryParseExact(
                input, availableDateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                out DateTime finalDate
                );
            DateTime date;
            if (check)
            {
                date = finalDate;
            }
            else
            {
                throw new ProjectException(input, "We couldn't parse your input into one of our date formats");
            }
            return date;

        }

        public double NumberParse(string number)
        {
            bool flag = double.TryParse(number, out double result);
            double FinalNumber;
            if (flag)
            {
                FinalNumber = result;
            }
            else
            {
                throw new ProjectException(number, "We couldn't parse your input into real number");
            }

            return FinalNumber;

        }

        public async Task<string> AllProjects()
        {
            var sp = new StringBuilder();

            var allProjects = await _unit.Projects.GetAll();
            foreach (var p in projects)
            {
                var names = new StringBuilder();
                p.Members.ForEach(x => names.AppendLine($"{x.Name}, "));
                bool flag = p.EndDate.HasValue;
                string days = flag ? (p.EndDate - p.StartDate).ToString() : " Unknown";
                sp.AppendLine($"Project: {p.ProjectName} with members: {names.ToString()}, Duration of project: {days}.");
            }

            return sp.ToString();
        }

        public string GroupedMembers()
        {
            var sp = new StringBuilder();

            var MembersBySpecialization = MembersInTeam.GroupBy(x => x.Specialization).ToList();

            foreach (var group in MembersBySpecialization)
            {
                sp.AppendLine($"Specialization: {group.Key}");
                foreach (var item in group)
                {
                    sp.AppendLine($"- {item.DetailFormat}");
                }
            }

            return sp.ToString();


        }
        public List<Project> LongProjects(string days)
        {

            if (!int.TryParse(days, out int result))
            {
                throw new ProjectException(days, "can't parse days");
            }
            var longProjects = projects.Where(x => x.EndDate.HasValue && (x.EndDate.Value - x.StartDate).TotalDays > result).ToList();


            return longProjects;
        }


        public List<TeamMember> NameLike(string str)
        {
            return MembersInTeam.Where(x => x.Name.Contains(str, StringComparison.OrdinalIgnoreCase)).ToList();

        }

    
 

    public async Task AddProject(Project item)
        {
            await _unit.Projects.Add(item);
            await _unit.CompleteSave();
        }


        public async Task AddPerson(Person osoba)
        {
            await _unit.People.Add(osoba);
            await _unit.CompleteSave();
        }

        public async Task updateProject(int id, Project updatedData)
        {
            var existingProject = await _unit.Projects.FindById(id);

            if(existingProject != null)
            {
                existingProject.ProjectName = updatedData.ProjectName;
                existingProject.StartDate = updatedData.StartDate;
                existingProject.EndDate = updatedData.EndDate;
            }
            else
            {
                throw new ProjectException($"Project id {id}", " can't be found");
            }
            await _unit.CompleteSave();
        }

        public async Task UpdatePerson(int id, TeamMember member)
        {
             var pers = await _unit.Member.FindById(id);
            if (pers != null) {
                pers.Name = member.Name;
                pers.Specialization = member.Specialization;
                pers.ExperienceInYears = member.ExperienceInYears;
                pers.ProjectId = member.ProjectId;

            }
            else
            {
                throw new ProjectException($"Project id {id}", " can't be found");
            }
            await _unit.CompleteSave();
        }

        public async Task DeleteProject(int id)
        {
            var itemToDelete = await _unit.Projects.FindById(id);
            _unit.Projects.Delete(itemToDelete); 
            await _unit.CompleteSave();

        }

        public async Task DeletePerson(int id)
        {
            var person = await _unit.People.FindById(id);
            _unit.People.Delete(person);
            await _unit.CompleteSave();
        } 


    }
}
