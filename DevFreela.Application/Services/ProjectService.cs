using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services;

public class ProjectService : IProjectService
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectService(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ResultViewModel<List<ProjectItemViewModel>> Get(string search, int page = 0, int rows = 3)
    {
        var projects = _dbContext.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Where(x => !x.IsDeleted && (search == "" || x.Title.Contains(search) || x.Description.Contains(search)))
            .Skip(page * rows) // pagination
            .Take(rows) // pagination
            .ToList();

        var model = projects.Select(ProjectItemViewModel.FromProject).ToList();

        return ResultViewModel<List<ProjectItemViewModel>>.Sucess(model);
    }

    public ResultViewModel<ProjectViewModel> GetById(int id)
    {
        var project = _dbContext.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Include(x => x.Comments)
            .SingleOrDefault(x => x.Id == id);

        if (project is null)
            return ResultViewModel<ProjectViewModel>.Error("Projeto não existe!!");

        var model = ProjectViewModel.FromProject(project);

        return ResultViewModel<ProjectViewModel>.Sucess(model);
    }

    public ResultViewModel<int> Insert(CreateProjectInputModel model)
    {
        var project = model.ToProject();

        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();

        return ResultViewModel<int>.Sucess(project.Id);
    }

    public ResultViewModel Update(UpdateProjectInputModel model)
    {
        var project = _dbContext.Projects.SingleOrDefault(x => x.Id == model.IdProject);

        if (project is null)
            return ResultViewModel.Error("Projeto não existe!!");

        project.Update(model.Title, model.Description, model.TotalCost);

        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();

        return ResultViewModel.Sucess();
    }

    public ResultViewModel Delete(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

        if (project is null)
            return ResultViewModel.Error("Projeto não existe!!");

        project.SetAsDeleted();

        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();

        return ResultViewModel.Sucess();
    }

    public ResultViewModel Start(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

        if (project is null)
            return ResultViewModel.Error("Projeto não existe!!");

        project.Start();

        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();

        return ResultViewModel.Sucess();
    }

    public ResultViewModel Complete(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

        if (project is null)
            return ResultViewModel.Error("Projeto não existe!!");

        project.Complete();

        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();

        return ResultViewModel.Sucess();
    }

    public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model)
    {
        var project = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

        if (project is null)
            return ResultViewModel.Error("Projeto não existe!!");

        var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

        _dbContext.ProjectComments.Add(comment);
        _dbContext.SaveChanges();

        return ResultViewModel.Sucess();
    }
}