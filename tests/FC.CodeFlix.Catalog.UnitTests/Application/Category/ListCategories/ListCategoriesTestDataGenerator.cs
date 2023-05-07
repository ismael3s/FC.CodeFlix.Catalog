using FC.CodeFlix.Catalog.Application.UseCases.Category.ListCategories;

namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.ListCategories;

public class ListCategoriesTestDataGenerator
{

    public static IEnumerable<object[]> GetInputWithoutAllParams(int times = 14)
    {

        var fixture = new ListCategoriesTestsFixture();
        var input = fixture.GetInput();
        for (int index = 0; index < times; index++)
        {
            switch (index % 7)
            {
                case 0:
                    yield return new object[] { new ListCategoriesInput() };
                    break;
                case 1:
                    yield return new object[] { new ListCategoriesInput(page: input.Page) };
                    break;
                case 2:
                    yield return new object[] { new ListCategoriesInput(page: input.Page, perPage: input.PerPage) };
                    break;

                case 3:
                    yield return new object[] { new ListCategoriesInput(
                        page: input.Page,
                        perPage: input.PerPage,
                        search: input.Search
                        )
                    };
                    break;
                case 4:
                    yield return new object[] { new ListCategoriesInput(
                        page: input.Page,
                        perPage: input.PerPage,
                        search: input.Search,
                        orderBy: input.OrderBy
                        )
                    };
                    break;

                case 5:
                    yield return new object[] { input };
                    break;
                default:
                    yield return new object[] { input };
                    break;
            }
        }

    }
}