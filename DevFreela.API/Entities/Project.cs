using DevFreela.API.Enums;

namespace DevFreela.API.Entities
{
    public class Project : BaseEntity
    {
        protected Project() { }

        public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost)
            : base()
        {
            ID_Client = idClient;
            ID_Freelancer = idFreelancer;
            Title = title;
            Description = description;
            TotalCost = totalCost;

            Status = ProjectStatusEnum.Created;
            Comments = new List<ProjectComment>();
        }

        public int ID_Client { get; set; }
        public int ID_Freelancer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public ProjectStatusEnum Status { get; set; }
        public User Client { get; set; }
        public User Freelancer { get; set; }
        public List<ProjectComment> Comments { get; set; }

        public void Cancel()
        {
            if (Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.Suspended)
            {
                Status = ProjectStatusEnum.Cancelled;
            }
        }

        public void Start()
        {
            if (Status == ProjectStatusEnum.Created)
            {
                Status = ProjectStatusEnum.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        public void Complete()
        {
            if (Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.PaymentPending)
            {
                Status = ProjectStatusEnum.Completed;
                CompletedAt = DateTime.Now;
            }
        }

        public void SetPaymentPending()
        {
            if (Status == ProjectStatusEnum.InProgress)
            {
                Status = ProjectStatusEnum.PaymentPending;
            }
        }

        public void Update(string title, string description, decimal totalCost)
        {
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }
    }
}
