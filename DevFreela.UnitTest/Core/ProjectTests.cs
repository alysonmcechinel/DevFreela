using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.UnitTest.Fakes;
using FluentAssertions;

namespace DevFreela.UnitTest;

public class ProjectTests
{
    [Fact]
    public void ProjectIsCreated_Start_Succes()
    {
        // Arrange
        //var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);
        // atento ao status
        var project = FakeDataHelper.CreateFakeProject();

        // Act
        project.Start();

        // Assert
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotNull(project.StartedAt);

        Assert.True(ProjectStatusEnum.InProgress == project.Status);
        Assert.False(project.StartedAt is null);

        // Assert with FluentAssertions
        project.Status.Should().Be(ProjectStatusEnum.InProgress);
        project.StartedAt.Should().NotBeNull();
    }

    [Fact]
    public void ProjectIsInvalidState_Start_ThrowException()
    {
        // Arrange
        //var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);
        var project = FakeDataHelper.CreateFakeProject();
        project.Start();

        // Act + Assert
        Action? start = project.Start;

        var exception = Assert.Throws<InvalidOperationException>(start);
        Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);

        // Assert with FluentAssertions
        start.Should().Throw<InvalidOperationException>().WithMessage(Project.INVALID_STATE_MESSAGE);
    }

    [Fact]
    public void ProjectIsInProgress_Complete_Succes()
    {
        // Arrange
        //var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);
        var project = FakeDataHelper.CreateFakeProject();
        project.Start();

        // Act
        project.Complete();

        // Assert
        Assert.Equal(ProjectStatusEnum.Completed, project.Status);
        Assert.NotNull(project.CompletedAt);
        Assert.True(project.CompletedAt is not null);

        // Assert with FluentAssertions
        project.Status.Should().Be(ProjectStatusEnum.Completed);
        project.CompletedAt.Should().NotBeNull(null);
    }

    [Fact]
    public void ProjectIsCreated_Complete_ThrowException()
    {
        // Arrange
        //var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);
        var project = FakeDataHelper.CreateFakeProject();

        // Act
        project.Complete();

        // Assert
        Assert.NotEqual(ProjectStatusEnum.Completed, project.Status);
        Assert.False(project.CompletedAt is not null);
    }

    [Fact]
    public void ProjectIsInProgress_SetPaymentPending_Succes()
    {
        // Arrange
        //var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);
        var project = FakeDataHelper.CreateFakeProject();
        project.Start();

        // Act
        project.SetPaymentPending();

        // Assert
        Assert.Equal(ProjectStatusEnum.PaymentPending, project.Status);
    }

    [Fact]
    public void ProjectIsCreated_SetPaymentPending_ThrowException()
    {
        // Arrange
        //var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);
        var project = FakeDataHelper.CreateFakeProject();

        // Act
        project.SetPaymentPending();

        // Assert
        Assert.NotEqual(ProjectStatusEnum.PaymentPending, project.Status);
        Assert.False(ProjectStatusEnum.PaymentPending == project.Status);
    }

    [Fact]
    public void Project_Update_Succes()
    {
        // Arrange
        //var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);
        var project = FakeDataHelper.CreateFakeProject();

        // Act
        project.Update("projeto legal", "projeto com c# e teste unitarios", 2500);

        // Assert
        Assert.Equal("projeto legal", project.Title);
        Assert.Equal("projeto com c# e teste unitarios", project.Description);
        Assert.Equal(2500, project.TotalCost);
        Assert.True(2500 == project.TotalCost);
    }

    [Fact]
    public void Project_Delete_Succes() 
    {
        // Arrange
        //var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);
        var project = FakeDataHelper.CreateFakeProject();

        // Act
        project.SetAsDeleted();

        // Assert
        Assert.True(project.IsDeleted);
    }
}
