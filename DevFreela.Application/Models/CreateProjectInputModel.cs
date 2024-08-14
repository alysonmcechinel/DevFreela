using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class CreateProjectInputModel
    {
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }

        public Project ToProject()
        {
            return new Project(IdClient, IdFreelancer, Title, Description, TotalCost);
        }
    }
}
