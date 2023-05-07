using FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.CodeFlix.Catalog.UnitTests.Application.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTestFixture : CategoryUseCasesBaseFixture
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


}


[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture> { }
