namespace DevFreela.API.Models
{
    public class CreateProjectInputModel
    {
        public int ID_Client { get; set; }
        public int ID_Freelancer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
    }
}
