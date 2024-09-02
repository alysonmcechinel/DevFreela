using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Project
{
    public class CompleteProjectCommand : IRequest<ResultViewModel>
    {
        public CompleteProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
