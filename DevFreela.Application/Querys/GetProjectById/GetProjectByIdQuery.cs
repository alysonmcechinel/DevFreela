using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Querys.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<ResultViewModel<ProjectViewModel>>
    {
        public GetProjectByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
