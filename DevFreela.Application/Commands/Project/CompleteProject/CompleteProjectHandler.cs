using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Project
{
    public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _projectRepository;

        public CompleteProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetById(request.Id);

            if (project is null)
                return ResultViewModel.Error("Projeto não existe!!");

            project.Complete();

            await _projectRepository.Update(project);

            return ResultViewModel.Sucess();
        }
    }
}
