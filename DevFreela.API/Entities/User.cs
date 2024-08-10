namespace DevFreela.API.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate)
            : base()
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Active = true;

            Skills = new List<UserSkill>();
            OwnedProjects = new List<Project>();
            FreelancerProjects = new List<Project>();
            Comments = new List<ProjectComment>();
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public List<UserSkill> Skills { get; set; }
        public List<Project> OwnedProjects { get; set; }
        public List<Project> FreelancerProjects { get; set; }
        public List<ProjectComment> Comments { get; set; }
    }
}
