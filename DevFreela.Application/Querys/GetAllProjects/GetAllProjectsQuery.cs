using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Querys.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
        public GetAllProjectsQuery(string search, int page, int rows)
        {
            Search = search;
            Page = page;
            Rows = rows;
        }

        public string Search { get; set; }
        public int Page { get; set; }
        public int Rows { get; set; }
    }
}
