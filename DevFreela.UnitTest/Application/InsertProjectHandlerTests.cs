using DevFreela.Application.Commands.Project;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using FluentAssertions;
using MediatR;
using Moq;
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

        // Assert with FluentAssertions
        result.IsSucess.Should().BeTrue();
        result.Data.Should().Be(ID);

        await repository.Received(1).Add(Arg.Any<Project>());
    }

    [Fact]
    public async Task InputDataAreOK_Insert_Success_Moq()
    {
        // Arrange
        const int ID = 1;

        // mesma coisa que o repository pode ser feito das duas maneiras.
        //var mock = new Mock<IProjectRepository>();
        //mock.Setup(x => x.Add(It.IsAny<Project>())).ReturnsAsync(ID);
        
        var repository = Mock.Of<IProjectRepository>(x => x.Add(It.IsAny<Project>()) == Task.FromResult(ID));
        var mediator = Mock.Of<IMediator>();

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

        //mock.Verify(x => x.Add(It.IsAny<Project>()), Times.Once);

        Mock.Get(repository).Verify(x => x.Add(It.IsAny<Project>()), Times.Once);
    }

    // TODO: implementar error com NSubstitute
}
