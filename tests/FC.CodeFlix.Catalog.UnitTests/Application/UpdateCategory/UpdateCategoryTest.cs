using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.CodeFlix.Catalog.Domain.Entities;
using FluentAssertions;
using Moq;
using UseCase = FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UpdateCategory;
[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory(DisplayName = nameof(ShouldBeAbleToUpdateACategory))]
    [Trait("Application", "UpdateCategory - Use Cases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.UpdateCategoryValidData),
        parameters: 20,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async void ShouldBeAbleToUpdateACategory(Category category, UpdateCategoryInput updateCategoryInput)
    {
        var unitOfWorkRepositoryMock = _fixture.GetUOWRepositoryMock();
        var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
        categoryRepositoryMock.Setup(
              repository => repository.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(category);

        var sut = new UseCase.UpdateCategory(categoryRepositoryMock.Object, unitOfWorkRepositoryMock.Object);

        var output = await sut.Handle(updateCategoryInput, CancellationToken.None);
        output.Should().NotBeNull();
        output.Id.Should().Be(category.Id);
        output.Name.Should().Be(updateCategoryInput.Name);
        output.Description.Should().Be(updateCategoryInput.Description);
        output.IsActive.Should().Be((bool)updateCategoryInput.IsActive!);
        categoryRepositoryMock.Verify(x => x.UpdateAsync(category, It.IsAny<CancellationToken>()), Times.Once);
        categoryRepositoryMock.Verify(x => x.GetAsync(category.Id, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkRepositoryMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory(DisplayName = nameof(ShouldBeAbleToUpdateACategoryWithoutProvidingIsActive))]
    [Trait("Application", "UpdateCategory - Use Cases")]
    [MemberData(
       nameof(UpdateCategoryTestDataGenerator.UpdateCategoryValidData),
       parameters: 20,
       MemberType = typeof(UpdateCategoryTestDataGenerator)
   )]
    public async void ShouldBeAbleToUpdateACategoryWithoutProvidingIsActive(Category category, UpdateCategoryInput updateCategoryInput)
    {
        var unitOfWorkRepositoryMock = _fixture.GetUOWRepositoryMock();
        var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
        categoryRepositoryMock.Setup(
              repository => repository.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(category);

        var sut = new UseCase.UpdateCategory(categoryRepositoryMock.Object, unitOfWorkRepositoryMock.Object);

        var input = updateCategoryInput with
        {
            IsActive = null
        };

        var output = await sut.Handle(input, CancellationToken.None);
        output.Should().NotBeNull();
        output.Id.Should().Be(category.Id);
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(category.IsActive);
        categoryRepositoryMock.Verify(x => x.UpdateAsync(category, It.IsAny<CancellationToken>()), Times.Once);
        categoryRepositoryMock.Verify(x => x.GetAsync(category.Id, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkRepositoryMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory(DisplayName = nameof(ShouldBeAbleToUpdateACategoryOnlyWithName))]
    [Trait("Application", "UpdateCategory - Use Cases")]
    [MemberData(
   nameof(UpdateCategoryTestDataGenerator.UpdateCategoryValidData),
   parameters: 20,
   MemberType = typeof(UpdateCategoryTestDataGenerator)
)]
    public async void ShouldBeAbleToUpdateACategoryOnlyWithName(Category category, UpdateCategoryInput updateCategoryInput)
    {
        var unitOfWorkRepositoryMock = _fixture.GetUOWRepositoryMock();
        var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
        categoryRepositoryMock.Setup(
              repository => repository.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(category);

        var sut = new UseCase.UpdateCategory(categoryRepositoryMock.Object, unitOfWorkRepositoryMock.Object);

        var input = updateCategoryInput with
        {
            Description = null,
            IsActive = null
        };

        var output = await sut.Handle(input, CancellationToken.None);
        output.Should().NotBeNull();
        output.Id.Should().Be(category.Id);
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(category.Description);
        output.IsActive.Should().Be(category.IsActive);
        categoryRepositoryMock.Verify(x => x.UpdateAsync(category, It.IsAny<CancellationToken>()), Times.Once);
        categoryRepositoryMock.Verify(x => x.GetAsync(category.Id, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkRepositoryMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = nameof(ShouldThrowErrorWhenTryToUpdateANotFoundCategoru))]
    [Trait("Application", "UpdateCategory - Use Cases")]
    public async void ShouldThrowErrorWhenTryToUpdateANotFoundCategoru()
    {
        var unitOfWorkRepositoryMock = _fixture.GetUOWRepositoryMock();
        var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
        var guid = Guid.NewGuid();
        categoryRepositoryMock.Setup(
              repository => repository.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )
        ).ThrowsAsync(new NotFoundException($"Category '{guid}' not found"));

        var sut = new UseCase.UpdateCategory(categoryRepositoryMock.Object, unitOfWorkRepositoryMock.Object);

        var input = new UpdateCategoryInput(guid, "Category Name", "Category Description", true);
        Func<Task> act = async () => await sut.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Category '{guid}' not found");
        categoryRepositoryMock.Verify(x => x.GetAsync(guid, It.IsAny<CancellationToken>()), Times.Once);
    }
}
