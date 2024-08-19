using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Querys.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
{
    private readonly DevFreelaDbContext _dbContext;

    public GetProjectByIdQueryHandler(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Include(x => x.Comments)
            .SingleOrDefaultAsync(x => x.Id == request.Id);

        if (project is null)
            return ResultViewModel<ProjectViewModel>.Error("Projeto não existe!!");

        var model = ProjectViewModel.FromProject(project);

        return ResultViewModel<ProjectViewModel>.Sucess(model);
    }
}