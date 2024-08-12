namespace DevFreela.API.Models
{
    public class CreateProjectCommentInputModel
    {
        public int ID_Project { get; set; }
        public int ID_User { get; set; }
        public string Content { get; set; }
    }
}
