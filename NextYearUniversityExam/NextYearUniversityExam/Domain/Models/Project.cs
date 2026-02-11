namespace Domain; 

public class Project { 
    public Project() { }
    
    public int Id { get; set; }
    public string ProjectName { get; set; }
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public List<TeamMember> Members { get; set; }



}




