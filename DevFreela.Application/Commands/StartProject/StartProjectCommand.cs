using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommand : IRequest<ResultViewModel>
    {
        public StartProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
