using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetAll(string search, int page, int rows)
        {
            var projects = await _dbContext.Projects
                .Include(x => x.Client)
                .Include(x => x.Freelancer)
                .Where(x => !x.IsDeleted 
                             && (search == "" || x.Title.Contains(search) || x.Description.Contains(search)))
                .Skip(page * rows) // pagination
                .Take(rows) // pagination
                .ToListAsync();

            return projects;
        }

        public async Task<Project?> GetDetailsById(int id)
        {
            var project = await _dbContext.Projects
                .Include(x => x.Client)
                .Include(x => x.Freelancer)
                .Include(x => x.Comments)
                .SingleOrDefaultAsync(x => x.Id == id);

            return project;
        }

        public async Task<Project?> GetById(int id)
        {
            var project = await _dbContext.Projects
                .SingleOrDefaultAsync(x => x.Id == id);

            return project;
        }

        public async Task<int> Add(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            return project.Id;
        }

        public async Task Update(Project project)
        {
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddComment(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Projects.AnyAsync(x => x.Id == id);
        }
    }
}
