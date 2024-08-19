using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;

namespace DevFreela.Application.Commands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IMediator _mediator;

        public InsertProjectHandler(DevFreelaDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToProject();

            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            var projectCreatedNotification = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(projectCreatedNotification);

            return ResultViewModel<int>.Sucess(project.Id);
        }
    }
}
