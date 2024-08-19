using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject
{
    public class ValidateProjectCommandBehavior : IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public ValidateProjectCommandBehavior(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExists = _dbContext.Users.Any(c => c.Id == request.IdClient);
            var freelancerExists = _dbContext.Users.Any(c => c.Id == request.IdFreelancer);
            if (!clientExists || !freelancerExists)
                return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos");

            if (request.TotalCost < 0)
                return ResultViewModel<int>.Error("O custo total do projeto não pode ser negativo");

            return await next();
        }
    }
}
