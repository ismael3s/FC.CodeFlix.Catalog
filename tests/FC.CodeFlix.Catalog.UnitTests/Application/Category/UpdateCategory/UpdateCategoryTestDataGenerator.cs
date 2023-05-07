using FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.UpdateCategory;
public class UpdateCategoryTestDataGenerator
{

    public static IEnumerable<object[]> UpdateCategoryValidData(int times = 10)
    {
        var fixture = new UpdateCategoryTestFixture();

        for (int i = 0; i < times; i++)
        {
            var category = fixture.GetCategory();
            var categoryToUpdateWith = fixture.GetCategory();
            var input = new UpdateCategoryInput(category.Id, categoryToUpdateWith.Name, categoryToUpdateWith.Description, categoryToUpdateWith.IsActive);

            yield return new object[] { category, input };
        }

    }
}
