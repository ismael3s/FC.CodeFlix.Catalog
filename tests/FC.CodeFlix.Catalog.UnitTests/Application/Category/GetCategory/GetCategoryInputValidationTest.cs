using FC.CodeFlix.Catalog.Application.UseCases.Category.GetCategory;
using FC.CodeFlix.Catalog.UnitTests.Application.Category.DeleteCategory;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.GetCategory;
[Collection(nameof(DeleteCategoryTestFixture))]
public class GetCategoryInputValidationTest
{
    public GetCategoryInputValidationTest()
    {
    }
    [Fact(DisplayName = nameof(ValidationOk))]
    [Trait("Application", "GetCategoryInputValidation - Use Cases")]
    public void ValidationOk()
    {
        var input = new GetCategoryInput(Guid.NewGuid());
        var validator = new GetCategoryInputValidator();

        var validationResult = validator.Validate(input);

        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = nameof(ValidationNotOk))]
    [Trait("Application", "GetCategoryInputValidation - Use Cases")]
    public void ValidationNotOk()
    {
        var input = new GetCategoryInput(Guid.Empty);
        var validator = new GetCategoryInputValidator();

        var validationResult = validator.Validate(input);

        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors
            .First()
            .ErrorMessage
            .Should()
            .Be("'Id' must not be empty.");
    }
}
