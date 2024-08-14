﻿using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class ProjectItemViewModel
    {
        public ProjectItemViewModel(int id, string title, string description, string clientName, string freelancerName, decimal totalCost)
        {
            Id = id;
            Title = title;
            ClientName = clientName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string ClientName { get; private set; }
        public string FreelancerName { get; set; }
        public decimal TotalCost { get; set; }

        public static ProjectItemViewModel FromProject(Project project)
        {
            return new ProjectItemViewModel(project.Id, project.Title, project.Description, project.Client.FullName, project.Freelancer.FullName, project.TotalCost);
        }
    }
}
