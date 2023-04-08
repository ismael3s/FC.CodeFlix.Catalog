using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.CodeFlix.Catalog.Domain.Repositories;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTestFixture : BaseFixture
{

    public CreateCategoryTestFixture() : base()
    {
    }

    public CreateCategoryInput GetValidCreateCategoryInput(bool? isActive = null)
    {
        var aName = Faker.Commerce.Categories(1).First();
        var aDescription = Faker.Commerce.ProductDescription();
        var aIsActive = isActive ?? GetRandomIsActive();

        return new CreateCategoryInput(aName, aDescription, aIsActive!);
    }


    public CreateCategoryInput GetInvalidCreateCategoryInput(bool? isActive = true)
    {
        var isEven = new Random().Next(1, 10) % 2 == 0;
        var aName = Faker.Lorem.Paragraph(2)[..(isEven ? 2 : 1)];
        string? aDescription = null;
        var aIsActive = isActive ?? GetRandomIsActive();

        return new CreateCategoryInput(aName, aDescription, aIsActive);
    }

    public string GetValidCategoryName()
    {
        string name;

        do
        {
            name = Faker.Commerce.ProductName();
        } while (name?.Length < 3 || name?.Length > 255);

        return name!;
    }

    public string GetValidDescription()
    {
        var description = Faker.Commerce.ProductDescription();

        if (description.Length > 10_000) return description[..(10_000)];

        return description;
    }

    public bool GetRandomIsActive() => new Random().NextDouble() < 0.5;

    public Mock<ICategoryRepository> GetCategoryRepositoryMock() => new();

    public Mock<IUnitOfWork> GetUOWRepositoryMock() => new();


}


[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture> { }
