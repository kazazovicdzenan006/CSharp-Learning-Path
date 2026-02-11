using Data.Interfaces;
using Domain;
using Domain.Exceptions;
using System.Text;


namespace Services.Service
{
    
    public class ProjectManagmentService { 
    private readonly IUnitOfWork _unit;
        private IQueryable<TeamMember> MembersInTeam => _unit.People.GetQueryable().OfType<TeamMember>();
        private IQueryable<Project> projects => _unit.Projects.GetQueryable().OfType<Project>();
        public ProjectManagmentService(IUnitOfWork unit) {
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
            if ( check )
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

            if(!int.TryParse(days, out int result))
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

    
    }



}
