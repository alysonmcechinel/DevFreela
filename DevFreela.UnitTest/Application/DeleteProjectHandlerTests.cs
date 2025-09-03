using DevFreela.Application.Commands.Project;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTest.Application;

public class DeleteProjectHandlerTests
{
    [Fact]
    public async Task ProjectExists_Delete_Succes_NSubstiture()
    {
        // Arrange
        var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);

        var repository = Substitute.For<IProjectRepository>();
        repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
        repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(1);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.True(result.IsSucess);
        await repository.Received(1).GetById(1);
        await repository.Received(1).Update(Arg.Any<Project>());
    }

    [Fact]
    public async Task ProjectDoesNotExist_Delete_Error_NSubstiture()
    {
        // Arrange
        var repository = Substitute.For<IProjectRepository>();
        repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(1);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.False(result.IsSucess);
        Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);

        await repository.Received(1).GetById(Arg.Any<int>());
        await repository.DidNotReceive().Update(Arg.Any<Project>());
    }
}
