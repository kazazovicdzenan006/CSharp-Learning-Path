namespace Domain;

public record TeamMember : Person
{
    public TeamMember() { }

    public TeamMember(int id, string name, string specialization, int experienceInYears, int projectId) {
        Id = id;
        Name = name;
        this.Specialization = specialization;
        this.ExperienceInYears = experienceInYears;
        this.ProjectId = projectId; 

    }

    public string Specialization { get; set; }
    
    public int ExperienceInYears { get; set; }

    public int ProjectId { get; set; }

    public string StandardFormat() => $"{Name} ({Specialization}, {ExperienceInYears} Years of Experience)";
    public string DetailFormat() => $"ID: {Id} | Name: {Name} | Position: {Specialization} | Experience: {ExperienceInYears} Years.";



}


