using FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.CodeFlix.Catalog.UnitTests.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTestFixture : BaseFixture
{

    public CreateCategoryTestFixture() : base()
    {
    }

    public CreateCategoryInput GetValidCreateCategoryInput(bool aIsActive = true)
    {
        var aName = Faker.Commerce.Categories(1).First();
        var aDescription = Faker.Commerce.ProductDescription();

        return new CreateCategoryInput(aName, aDescription, aIsActive);
    }


    public CreateCategoryInput GetInvalidCreateCategoryInput(bool aIsActive = true)
    {
        var isEven = new Random().Next(1, 10) % 2 == 0;
        var aName = Faker.Lorem.Paragraph(2)[..(isEven ? 2 : 1)];
        string? aDescription = null;

        return new CreateCategoryInput(aName, aDescription, aIsActive);
    }
}



[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture> { }
