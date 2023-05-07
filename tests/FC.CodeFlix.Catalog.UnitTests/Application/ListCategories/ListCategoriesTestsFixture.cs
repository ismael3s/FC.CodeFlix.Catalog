using FC.CodeFlix.Catalog.Application.UseCases.Category.ListCategories;
using FC.CodeFlix.Catalog.UnitTests.Application.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.ListCategories;
[CollectionDefinition(nameof(ListCategoriesTestsFixture))]
public class ListCategoriesTestsFixtureCollection : ICollectionFixture<ListCategoriesTestsFixture>
{
}
public class ListCategoriesTestsFixture : CategoryUseCasesBaseFixture
{
    public ListCategoriesTestsFixture()
    {
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
        );
    }
}
