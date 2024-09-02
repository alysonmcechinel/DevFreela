using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Project
{
    public class InsertCommentCommand : IRequest<ResultViewModel>
    {
        public int IdProject { get; set; }
        public int IdUser { get; set; }
        public string Content { get; set; }
    }
}
