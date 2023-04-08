using Bogus;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FluentAssertions;
using DomainEntities = FC.CodeFlix.Catalog.Domain.Entities;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entities.Category;
[Collection(nameof(CategoryTestFixture))]
public partial class CategoryTest
{
    private readonly CategoryTestFixture _categoryTestFixture;
    public CategoryTest(CategoryTestFixture categoryTestFixture)
    {
        Randomizer.Seed = new Random();
        _categoryTestFixture = categoryTestFixture;
    }

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        var validData = _categoryTestFixture.BuildValidCategory();


        var category = DomainEntities.Category.Create(validData.Name, validData.Description);
        var dateTimeAfter = DateTime.Now;


        category.Name
            .Should()
            .Be(validData.Name);

        category.Description
            .Should()
            .Be(validData.Description);

        category.Id
            .Should()
            .NotBe(Guid.Empty);

        category.CreatedAt
            .Should()
            .NotBe(default);

        category.CreatedAt
            .Should()
            .BeBefore(dateTimeAfter);

        category.IsActive.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(ShouldInstantiateCategory_WithIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void ShouldInstantiateCategory_WithIsActive(bool isActive)
    {
        var validData = _categoryTestFixture.BuildValidCategory();


        var category = DomainEntities.Category.Create(validData.Name, validData.Description, isActive);
        var dateTimeAfter = DateTime.Now.AddSeconds(1);


        category.Name
            .Should()
            .Be(validData.Name);

        category.Description
            .Should()
            .Be(validData.Description);

        category.Id
            .Should()
            .NotBe(Guid.Empty);

        category.CreatedAt
            .Should()
            .NotBe(default);

        category.CreatedAt
            .Should()
            .BeBefore(dateTimeAfter);

        category.IsActive.Should().Be(isActive);
    }

    [Theory(DisplayName = nameof(ShouldNotBeAbleToInstantiateACategory_When_NameIsNullOrEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("       ")]
    public void ShouldNotBeAbleToInstantiateACategory_When_NameIsNullOrEmpty(string? name)
    {
        var validData = _categoryTestFixture.BuildValidCategory();


        Action action = () => DomainEntities.Category.Create(name!, validData.Description);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");

    }


    [Theory(DisplayName = nameof(ShouldNotBeAbleToInstantiateACategory_When_DescriptionIsNull))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(null)]
    public void ShouldNotBeAbleToInstantiateACategory_When_DescriptionIsNull(string? description)
    {
        var validData = _categoryTestFixture.BuildValidCategory();


        Action action = () => DomainEntities.Category.Create(validData.Name, description!);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should not be null");
    }

    [Theory(DisplayName = nameof(ShouldNotBeAbleToInstantiateACategory_When_NameIsLesserThan3Characteres))]
    [Trait("Domain", "Category - Aggregates")]
    [MemberData(nameof(GetNameWithLessThan3Characteres), parameters: 10)]
    public void ShouldNotBeAbleToInstantiateACategory_When_NameIsLesserThan3Characteres(string name)
    {
        var validData = _categoryTestFixture.BuildValidCategory();


        Action action = () => DomainEntities.Category.Create(name, validData.Description);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be less than 3 characteres");
    }

    [Fact(DisplayName = nameof(ShouldNotBeAbleToInstantiateACategory_When_NameIsGreatherThan255Characteres))]
    [Trait("Domain", "Category - Aggregates")]
    public void ShouldNotBeAbleToInstantiateACategory_When_NameIsGreatherThan255Characteres()
    {
        var validData = _categoryTestFixture.BuildValidCategory();

        var invalidName = string.Join(null, Enumerable.Range(0, 256)
            .Select(_ => "a")
            .ToArray()
           );


        Action action = () => DomainEntities.Category.Create(invalidName!, validData.Description);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be greather than 255 characteres");
    }

    [Fact(DisplayName = nameof(ShouldNotBeAbleToInstantiateACategory_When_DescriptionIsGreatherThan10_000Characteres))]
    [Trait("Domain", "Category - Aggregates")]
    public void ShouldNotBeAbleToInstantiateACategory_When_DescriptionIsGreatherThan10_000Characteres()
    {

        var invalidDescription = string.Join(null, Enumerable.Range(1, 10001)
            .Select(_ => "a")
            .ToArray()
           );


        Action action = () => DomainEntities.Category.Create("ValidName", invalidDescription);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should not be greather than 10000 characteres");
    }

    [Fact(DisplayName = nameof(ShouldBeAbleToActivate_When_ActivateMethod_IsCalled))]
    [Trait("Domain", "Category - Aggregates")]
    public void ShouldBeAbleToActivate_When_ActivateMethod_IsCalled()
    {
        var category = new CategoryTestFixture()
            .WithName()
            .WithIsActive(false)
            .Build();

        category.Activate();

        category.IsActive.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(ShouldBeAbleToActivate_When_DeactivateMethod_IsCalled))]
    [Trait("Domain", "Category - Aggregates")]
    public void ShouldBeAbleToActivate_When_DeactivateMethod_IsCalled()
    {
        var category = new CategoryTestFixture()
            .WithName()
            .WithIsActive(true)
            .Build();

        category.Deactivate();

        category.IsActive.Should().BeFalse();
    }


    [Fact(DisplayName = nameof(ShouldUpdateCategory_When_ValidDataIsProvide))]
    [Trait("Domain", "Category - Aggregates")]
    public void ShouldUpdateCategory_When_ValidDataIsProvide()
    {
        var category = _categoryTestFixture.BuildValidCategory();

        var newData = _categoryTestFixture.BuildValidCategory();

        category.Update(newData.Name, newData.Description);

        category.Name.Should().Be(newData.Name);
        category.Description.Should().Be(newData.Description);
    }

    [Fact(DisplayName = nameof(ShouldUpdateOnlyCategoryName_When_ValidDataIsProvide))]
    [Trait("Domain", "Category - Aggregates")]
    public void ShouldUpdateOnlyCategoryName_When_ValidDataIsProvide()
    {
        var category = _categoryTestFixture.BuildValidCategory();

        var originalDescription = category.Description;

        var newData = _categoryTestFixture.BuildValidCategory();

        category.Update(newData.Name);

        category.Name.Should().Be(newData.Name);
        category.Description.Should().Be(originalDescription);
    }

    [Theory(DisplayName = nameof(ShouldThrowError_When_TryToUpdateName_WithInvalidData))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("       ")]
    public void ShouldThrowError_When_TryToUpdateName_WithInvalidData(string aName)
    {
        var category = _categoryTestFixture.BuildValidCategory();

        Action action = () => category.Update(aName!);

        action.Should().Throw<EntityValidationException>();
    }
}
