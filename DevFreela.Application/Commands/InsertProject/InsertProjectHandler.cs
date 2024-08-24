﻿using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.Repositories;

namespace DevFreela.Application.Commands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMediator _mediator;

        public InsertProjectHandler(IProjectRepository projectRepository, IMediator mediator)
        {
            _projectRepository = projectRepository;
            _mediator = mediator;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToProject();

            await _projectRepository.Add(project);

            var projectCreatedNotification = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(projectCreatedNotification);

            return ResultViewModel<int>.Sucess(project.Id);
        }
    }
}
