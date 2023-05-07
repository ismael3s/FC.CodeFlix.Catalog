using FC.CodeFlix.Catalog.Application.UseCases.Category.ListCategories;
using FC.CodeFlix.Catalog.Domain.Entities;
using FC.CodeFlix.Catalog.Domain.Repositories;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.ListCategories;
[CollectionDefinition(nameof(ListCategoriesTestsFixture))]
public class ListCategoriesTestsFixtureCollection : ICollectionFixture<ListCategoriesTestsFixture>
{
}
public class ListCategoriesTestsFixture : BaseFixture
{
    public ListCategoriesTestsFixture()
    {
    }

    public Mock<ICategoryRepository> GetRepositoryMock() => new();

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

    public Category GetCategory()
    {
        return new Category(GetValidCategoryName(), GetValidDescription(), GetRandomIsActive());
    }

    public IEnumerable<Category> GetCategories(int amount = 5)
    {
        for (int index = 0; index < amount; index++)
        {
            yield return GetCategory();
        }
    }

    public ListCategoriesInput GetInput()
    {
        var random = new Random();

        return new ListCategoriesInput(
            page: random.Next(1, 10),
            perPage: random.Next(15, 60),
            search: Faker.Commerce.ProductName(),
            orderBy: Faker.Commerce.ProductName(),
            dir: random.Next(0, 10) > 5
                ? Catalog.Domain.SeedWork.SearchableRepository.SearchOrder.Asc
                : Catalog.Domain.SeedWork.SearchableRepository.SearchOrder.Desc
        ); ;
    }
}
