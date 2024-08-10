namespace DevFreela.API.Entities
{
    public class ProjectComment : BaseEntity
    {
        public ProjectComment(string content, int id_Project, int id_User)
            : base()
        {
            Content = content;
            ID_Project = id_Project;
            ID_User = id_User;
        }

        public int ID_Project { get; set; }
        public int ID_User { get; set; }
        public string Content { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }
    }
}
