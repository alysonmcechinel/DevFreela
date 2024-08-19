using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Querys.GetAllProjects;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    private readonly DevFreelaDbContext _dbContext;

    public GetAllProjectsQueryHandler(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _dbContext.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Where(x => !x.IsDeleted && (request.Search == "" || x.Title.Contains(request.Search) || x.Description.Contains(request.Search)))
            .Skip(request.Page * request.Rows) // pagination
            .Take(request.Rows) // pagination
            .ToListAsync();

        var model = projects.Select(ProjectItemViewModel.FromProject).ToList();

        return ResultViewModel<List<ProjectItemViewModel>>.Sucess(model);
    }
}