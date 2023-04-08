using FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryDataGenerator
{

    public static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new CreateCategoryTestFixture();
        var list = new List<object[]>();

        var input = fixture.GetValidCreateCategoryInput();

        var inputWithNameLessThan3Char = input with
        {
            Name = input.Name[..2]
        };

        list.Add(new object[] { inputWithNameLessThan3Char, "Name should not be less than 3 characteres" });

        var inputWithNameGreaterThan255Char = input with
        {
            Name = fixture.Faker.Lorem.Paragraphs(15)
        };

        list.Add(new object[] { inputWithNameGreaterThan255Char, "Name should not be greather than 255 characteres" });

        var inputWithNullName = input with
        {
            Name = null!
        };

        list.Add(new object[] { inputWithNullName, "Name should not be empty or null" });


        var inputWithNullDescription = input with
        {
            Description = null!
        };

        list.Add(new object[] { inputWithNullDescription, "Description should not be null" });


        var inputWithNullDescriptionGreaterThan10_000Char = input with
        {
            Description = fixture.Faker.Lorem.Paragraphs(100)
        };

        list.Add(new object[] { inputWithNullDescriptionGreaterThan10_000Char, "Description should not be greather than 10000 characteres" });

        return list;
    }

    public static IEnumerable<object[]> GetValidInputParams()
    {

        var fixture = new CreateCategoryTestFixture();

        var list = new List<object[]>();

        var validInput = fixture.GetValidCreateCategoryInput();

        list.Add(new object[] { validInput });

        var validInputWithIsActiveFalse = validInput with { IsActive = false };

        list.Add(new object[] { validInputWithIsActiveFalse });

        var validInputWithIsActiveTrue = validInput with { IsActive = true };

        list.Add(new object[] { validInputWithIsActiveTrue });


        var inputWithEmptyDescription = new CreateCategoryInput(validInput.Name);
        list.Add(new object[] { inputWithEmptyDescription });

        return list;
    }
}
