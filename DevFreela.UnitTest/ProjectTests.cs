using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTest;

public class ProjectTests
{
    [Fact]
    public void ProjectIsCreated_Start_Succes()
    {
        // Arrange
        var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);

        // Act
        project.Start();

        // Assert
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotNull(project.StartedAt);

        Assert.True(ProjectStatusEnum.InProgress == project.Status);
        Assert.False(project.StartedAt is null);
    }

    [Fact]
    public void ProjectIsInvalidState_Start_ThrowException()
    {
        // Arrange
        var project = new Project(1, 1, "Teste projeto", "Isso é uma descrição legal", 1000);
        project.Start();

        // Act + Assert
        Action? start = project.Start;

        var exception = Assert.Throws<InvalidOperationException>(start);
        Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
    }

    // TODO: fazer teste unitarios com Complete, SetPaymentPending, Update, SetAsDeleted
}
