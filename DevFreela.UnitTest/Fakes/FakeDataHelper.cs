using Bogus;
using DevFreela.Application.Commands.Project;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTest.Fakes;

public class FakeDataHelper
{
    private static readonly Faker _faker = new Faker();

    public static Project CreateFakeProjectV1()
    {
        return new Project(
            _faker.Random.Int(1 , 100),
            _faker.Random.Int(1 , 100),
            _faker.Commerce.ProductName(),
            _faker.Lorem.Sentence(),
            _faker.Random.Decimal(1000, 10000)
            );
    }

    private static readonly Faker<Project> _projectFaker = new Faker<Project>()
        .CustomInstantiator(f => new Project(
            f.Random.Int(1, 100),
            f.Random.Int(1, 100),
            f.Commerce.ProductName(),
            f.Lorem.Sentence(),
            f.Random.Decimal(1000, 10000)
            ));

    private static readonly Faker<InsertProjectCommand> _insertProjectCommand = new Faker<InsertProjectCommand>()
        .RuleFor(r => r.IdClient, f => f.Random.Int(1, 100))
        .RuleFor(r => r.IdFreelancer, f => f.Random.Int(1, 100))
        .RuleFor(r => r.Title, f => f.Commerce.ProductName())
        .RuleFor(r => r.Description, f => f.Lorem.Sentence())
        .RuleFor(r => r.TotalCost, f => f.Random.Decimal(1000, 15000));

    public static Project CreateFakeProject() => _projectFaker.Generate();

    public static List<Project> CreateFakeProjectList() => _projectFaker.Generate(5);

    public static InsertProjectCommand CreateFakeInsertProjectCommand() => _insertProjectCommand.Generate();

    // precisamos realmente disso??
    public static DeleteProjectCommand CreateFakeDeleteProjectCommand(int id) => new(id);
}
