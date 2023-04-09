using FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UpdateCategory;
[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryInputValidationTest
{

    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryInputValidationTest(UpdateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ShoudlReturnErrorsWhenInputIsInvalid))]
    [Trait("Application", "UpdateCategoryInputValidation - Use Cases")]
    public void ShoudlReturnErrorsWhenInputIsInvalid()
    {
        var input = new UpdateCategoryInput(Guid.Empty, null!, null, null);
        var validator = new UpdateCategoryInputValidation();
        var result = validator.Validate(input);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
        result.Errors.Should().Contain(x => x.PropertyName == nameof(input.Id));
        result.Errors.Should().Contain(x => x.PropertyName == nameof(input.Name));
    }


    [Fact(DisplayName = nameof(ShouldDoNothingWhenInputIsValid))]
    [Trait("Application", "UpdateCategoryInputValidation - Use Cases")]
    public void ShouldDoNothingWhenInputIsValid()
    {
        var validCategoryName = _fixture.GetValidCategoryName();
        var input = new UpdateCategoryInput(Guid.NewGuid(), validCategoryName, null, null);
        var validator = new UpdateCategoryInputValidation();
        var result = validator.Validate(input);
        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = nameof(ShoudlReturnErrorsWhenInputIsInvalidWithEmptyId))]
    [Trait("Application", "UpdateCategoryInputValidation - Use Cases")]
    public void ShoudlReturnErrorsWhenInputIsInvalidWithEmptyId()
    {
        var validCategoryName = _fixture.GetValidCategoryName();
        var input = new UpdateCategoryInput(Guid.Empty, validCategoryName, null, null);
        var validator = new UpdateCategoryInputValidation();
        var result = validator.Validate(input);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain(x => x.PropertyName == nameof(input.Id));
    }

    [Fact(DisplayName = nameof(ShoudlReturnErrorsWhenInputIsInvalidWithEmptyName))]
    [Trait("Application", "UpdateCategoryInputValidation - Use Cases")]
    public void ShoudlReturnErrorsWhenInputIsInvalidWithEmptyName()
    {
        var input = new UpdateCategoryInput(Guid.NewGuid(), null!, null, null);
        var validator = new UpdateCategoryInputValidation();
        var result = validator.Validate(input);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain(x => x.PropertyName == nameof(input.Name));
    }


}
