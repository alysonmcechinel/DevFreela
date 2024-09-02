using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Querys.Project;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetDetailsById(request.Id);

        if (project is null)
            return ResultViewModel<ProjectViewModel>.Error("Projeto não existe!!");

        var model = ProjectViewModel.FromProject(project);

        return ResultViewModel<ProjectViewModel>.Sucess(model);
    }
}