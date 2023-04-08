using Bogus;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Domain.Validation;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Validation;
public partial class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(ShouldNotThrowError_WhenTheDataIsNotNull))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void ShouldNotThrowError_WhenTheDataIsNotNull()
    {
        var value = Faker.Lorem.Word();
        Action action = () => DomainValidation.IsNotNull(value, "Value");

        action.Should().NotThrow();
    }


    [Fact(DisplayName = nameof(ShouldThrowError_WhenTheDataIsNull))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void ShouldThrowError_WhenTheDataIsNull()
    {
        var fieldName = Faker.Internet.UserName();
        Action action = () => DomainValidation.IsNotNull(null!, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be null");
    }


    [Fact(DisplayName = nameof(IsNotNullOrEmpty_ShouldNotThrowError_WhenTheDataIsNotEmpty))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void IsNotNullOrEmpty_ShouldNotThrowError_WhenTheDataIsNotEmpty()
    {
        var value = Faker.Lorem.Word();
        var fieldName = Faker.Internet.UserName();
        Action action = () => DomainValidation.IsNotNullOrEmpty(value, fieldName);

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(IsNotNullOrEmpty_ShouldThrowError_WhenTheDataIsEmpty))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void IsNotNullOrEmpty_ShouldThrowError_WhenTheDataIsEmpty(string? value)
    {
        var fieldName = Faker.Internet.UserName();
        Action action = () => DomainValidation.IsNotNullOrEmpty(value!, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be null or empty");
    }

    [Theory(DisplayName = nameof(MinLength_ShouldThrowError_WhenTheIsLesserThanSpecifiedMinLength))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(MinLengthTestThrowParams), parameters: 6)]
    public void MinLength_ShouldThrowError_WhenTheIsLesserThanSpecifiedMinLength(string? value, int minLength)
    {
        var fieldName = Faker.Internet.UserName();
        Action action = () => DomainValidation.MinLength(value!, minLength, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be less than {minLength} characteres");
    }

    [Theory(DisplayName = nameof(MinLength_ShouldNotThrowError_WhenValueIsGreatherThanSpecifiedMinLength))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(MinLengthTestNotThrowParams), parameters: 6)]
    public void MinLength_ShouldNotThrowError_WhenValueIsGreatherThanSpecifiedMinLength(string value, int minLength)
    {
        var fieldName = Faker.Internet.UserName();
        Action action = () => DomainValidation.MinLength(value!, minLength, fieldName);

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(MaxLength_ShouldNotThrowError_WhenValueIsLesserThanSpecifiedMaxLength))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(MaxLengthTestNotThrowParams), parameters: 6)]
    public void MaxLength_ShouldNotThrowError_WhenValueIsLesserThanSpecifiedMaxLength(string value, int maxLength)
    {
        var fieldName = Faker.Internet.UserName();
        Action action = () => DomainValidation.MaxLength(value!, maxLength, fieldName);

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(MaxLength_ShouldThrowError_WhenValueIsGreaterThanSpecifiedMaxLength))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(MaxLengthTestThrowParams), parameters: 6)]
    public void MaxLength_ShouldThrowError_WhenValueIsGreaterThanSpecifiedMaxLength(string? value, int maxLength)
    {
        var fieldName = Faker.Internet.UserName();
        Action action = () => DomainValidation.MaxLength(value!, maxLength, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be greather than {maxLength} characteres");
    }


}
