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

        public int ID_Project { get; private set; }
        public int ID_User { get; private set; }
        public string Content { get; private set; }
        public Project Project { get; private set; }
        public User User { get; private set; }
    }
}
