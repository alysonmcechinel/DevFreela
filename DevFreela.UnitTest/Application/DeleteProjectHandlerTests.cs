using DevFreela.Application.Commands.Project;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using FluentAssertions;
using Moq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

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

        // Assert with FluentAssertions
        result.IsSucess.Should().BeFalse();

        await repository.Received(1).GetById(Arg.Any<int>());
        await repository.DidNotReceive().Update(Arg.Any<Project>());
    }

    /*-------------------------------------------------------------------------------------------------*/

    [Fact]
    public async Task ProjectExists_Delete_Succes_Moq()
    {
        // Arrange
        var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);

        var repository = Mock.Of<IProjectRepository>(
            x => x.GetById(It.IsAny<int>()) == Task.FromResult(project)
            && x.Update(It.IsAny<Project>()) == Task.CompletedTask
        );

        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(1);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.True(result.IsSucess);
        Mock.Get(repository).Verify(v => v.GetById(1), Times.Once);
        Mock.Get(repository).Verify(v => v.Update(It.IsAny<Project>()), Times.Once);
    }

    [Fact]
    public async Task ProjectDoesNotExist_Delete_Error_Moq()
    {
        // Arrange
        var repository = Mock.Of<IProjectRepository>(
            x => x.GetById(It.IsAny<int>()) == Task.FromResult((Project?) null)
        );

        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(1);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.False(result.IsSucess);
        Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);

        Mock.Get(repository).Verify(v => v.GetById(1), Times.Once);
        Mock.Get(repository).Verify(v => v.Update(It.IsAny<Project>()), Times.Never);
    }
}
