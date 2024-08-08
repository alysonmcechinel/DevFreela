namespace DevFreela.API.Models
{
    public class UpdateProjectViewModel
    {
        public int ID_Project { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
    }
}
