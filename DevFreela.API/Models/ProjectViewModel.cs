using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int id, int idClient, int idFreelancer, string title, string description, string clientName, decimal totalCost, List<ProjectComment> comments)
        {
            Id = id;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            Title = title;
            Description = description;
            ClientName = clientName;
            TotalCost = totalCost;
            Comments = comments.Select(x => x.Content).ToList();
        }

        public int Id { get; private set; }
        public int IdClient { get; private set; }
        public int IdFreelancer { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ClientName { get; private set; }
        public string FreelancerName { get; set; }
        public decimal TotalCost { get; set; }
        public List<string> Comments { get; private set; }

        public static ProjectViewModel FromProject(Project entity)
        {
            return new (entity.Id, entity.IdClient, entity.IdFreelancer, entity.Title, entity.Description, entity.Client.FullName, entity.TotalCost, entity.Comments);
        }
    }
}
