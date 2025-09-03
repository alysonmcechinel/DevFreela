using DevFreela.Application.Commands.Project;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using NSubstitute;

namespace DevFreela.UnitTest.Application;

public class InsertProjectHandlerTests
{
    [Fact]
    public async Task InputDataAreOK_Insert_Success_NSubstitute()
    {
        // Arrange
        const int ID = 1;
        var mediator = Substitute.For<IMediator>();
        var repository = Substitute.For<IProjectRepository>();
        repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(1));

        var command = new InsertProjectCommand
        {
            Title = "Projeto Teste",
            Description = "Uma descrição legal do projeto",
            TotalCost = 2500,
            IdClient = 1,
            IdFreelancer = 1
        };

        var handler = new InsertProjectHandler(repository, mediator);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.True(result.IsSucess);
        Assert.Equal(ID, result.Data);
        await repository.Received(1).Add(Arg.Any<Project>());
    }

    // TODO: implementar error
}
