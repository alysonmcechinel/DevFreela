using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Project
{
    public class InsertProjectCommand : IRequest<ResultViewModel<int>>
    {
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }

        public Core.Entities.Project ToProject()
        {
            return new Core.Entities.Project(IdClient, IdFreelancer, Title, Description, TotalCost);
        }
    }
}
