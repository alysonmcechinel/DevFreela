﻿using DevFreela.API.Enums;

namespace DevFreela.API.Entities
{
    public class Project : BaseEntity
    {
        protected Project() { }

        public Project(int idClient, int idFreelancer, string title, string description, decimal totalCost)
            : base()
        {
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            Title = title;
            Description = description;
            TotalCost = totalCost;
            Status = ProjectStatusEnum.Created;
            Comments = new List<ProjectComment>();
        }

        public int IdClient { get; private set; }
        public int IdFreelancer { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public ProjectStatusEnum Status { get; private set; }
        public User Client { get; private set; }
        public User Freelancer { get; private set; }
        public List<ProjectComment> Comments { get; private set; }

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

        public void SetAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
