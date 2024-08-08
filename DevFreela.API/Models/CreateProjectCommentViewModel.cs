namespace DevFreela.API.Models
{
    public class CreateProjectCommentViewModel
    {
        public int ID_Project { get; set; }
        public int ID_User { get; set; }
        public string Content { get; set; }
    }
}
