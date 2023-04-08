namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entities.Category;
public partial class CategoryTest
{

    public static IEnumerable<object[]> GetNameWithLessThan3Characteres(int numberOfTests = 6)
    {
        var fixture = new CategoryTestFixture();

        for (int i = 0; i < numberOfTests; i++)
        {
            var isEven = i % 2 == 0;
            yield return new object[] { fixture.Faker.Lorem.Paragraph()[..(isEven ? 2 : 1)] };
        }
    }
}
