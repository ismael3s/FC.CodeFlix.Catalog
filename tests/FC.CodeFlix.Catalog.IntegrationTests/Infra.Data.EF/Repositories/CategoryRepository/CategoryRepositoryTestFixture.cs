using FC.CodeFlix.Catalog.Domain.Entities;
using FC.CodeFlix.Catalog.Infra.Data.EF;
using FC.CodeFlix.Catalog.IntegrationTests.Base;
using Microsoft.EntityFrameworkCore;
using SUT = FC.CodeFlix.Catalog.Infra.Data.EF.Repositories.CategoryRepository;
namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository;

public class CategoryRepositoryTestFixture : BaseFixture
{
    public CategoryRepositoryTestFixture() : base()
    {

    }

    public CodeflixCatalogDbContext CreateDbContext(bool preserveData = false)
    {
        var context = new CodeflixCatalogDbContext(
            new DbContextOptionsBuilder<CodeflixCatalogDbContext>()
                       .UseInMemoryDatabase("integration-tests-db")
                       .Options
                );

        if (preserveData == false) context.Database.EnsureDeleted();

        return context;
    }

    public string GenerateValidCategoryNameDifferentFrom(string aName)
    {
        var categoryName = GetValidCategoryName();
        while (categoryName == aName)
        {
            categoryName = GetValidCategoryName();
        }
        return categoryName;
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

        if (description.Length > 10_000) return description[..10_000];

        return description;
    }

    public bool GetRandomIsActive() => new Random().NextDouble() < 0.5;


    public Category GetCategory()
    {
        return Category.Create(GetValidCategoryName(), GetValidDescription(), GetRandomIsActive());
    }

    public IEnumerable<Category> GetCategories(int amount = 5)
    {
        for (int index = 0; index < amount; index++)
        {
            yield return GetCategory();
        }
    }

    public List<Category> GetCategoriesWithNames(List<string> names)
    => names.Select(name =>
    {
        var category = GetCategory();
        category.Update(name);
        return category;
    }).ToList();

    public SUT GetSUT(CodeflixCatalogDbContext dbContext) => new(dbContext);

}


[CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryFixtureCollection : ICollectionFixture<CategoryRepositoryTestFixture> { }
