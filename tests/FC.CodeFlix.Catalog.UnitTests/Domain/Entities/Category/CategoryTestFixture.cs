using Bogus;
using FC.CodeFlix.Catalog.UnitTests.Common;
using DomainEntities = FC.CodeFlix.Catalog.Domain.Entities;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entities.Category;
public class CategoryTestFixture : BaseFixture
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = "Valid Description";

    public bool IsActive { get; private set; } = true;

    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public CategoryTestFixture() : base()
    {
        Randomizer.Seed = new Random();
    }

    public CategoryTestFixture WithName()
    {
        do
        {
            Name = Faker.Commerce.Categories(1).First();
        } while (Name.Length < 3 || Name.Length > 255);

        return this;
    }

    public CategoryTestFixture WithName(string aName)
    {
        Name = aName;

        return this;
    }

    public CategoryTestFixture WithDescription()
    {
        Description = Faker.Commerce.ProductDescription();

        return this;
    }

    public CategoryTestFixture WithDescription(string aDescription)
    {
        Description = aDescription;

        return this;
    }

    public CategoryTestFixture WithIsActive(bool aActive = true)
    {
        IsActive = aActive;

        return this;
    }

    public DomainEntities.Category Build()
    {
        return DomainEntities.Category.Create(Name, Description, IsActive);
    }

    public DomainEntities.Category BuildValidCategory()
    {
        return new CategoryTestFixture().WithName().WithDescription().Build();
    }

    public DomainEntities.Category BuildInvalidCategoryWithNullFields()
    {
        return new CategoryTestFixture()
            .WithDescription(null!)
            .WithName(null!)
            .Build();
    }


}



[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> { }