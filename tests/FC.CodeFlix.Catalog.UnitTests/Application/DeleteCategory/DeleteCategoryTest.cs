using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
using FC.CodeFlix.Catalog.UnitTests.Application.GetCategory;
using FluentAssertions;
using Moq;
using UseCases = FC.CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
namespace FC.CodeFlix.Catalog.UnitTests.Application.DeleteCategory;
[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest
{
    private readonly DeleteCategoryTestFixture _fixture;

    public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ShouldBeAbleToDeleteACategory))]
    [Trait("Application", "DeleteCategory - Use Cases")]
    public async void ShouldBeAbleToDeleteACategory()
    {
        var unitOfWorkRepositoryMock = _fixture.GetUnitOfWorkMockRepository();
        var categoryRepositoryMock = _fixture.GetCategoryMockRepository();
        var category = _fixture.GetCategory();

        categoryRepositoryMock.Setup(
            repository => repository.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                )
        ).ReturnsAsync(category);

        var sut = new UseCases.DeleteCategory(
            categoryRepositoryMock.Object,
            unitOfWorkRepositoryMock.Object
        );
        var input = new DeleteCategoryInput(category.Id);

        var output = await sut.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Id.Should().Be(category.Id);
        output.Name.Should().Be(category.Name);
        output.Description.Should().Be(category.Description);

        unitOfWorkRepositoryMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);

        categoryRepositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        categoryRepositoryMock.Verify(x => x.DeleteAsync(category, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = nameof(ShoudlThrowAnErrorWhenTryToDeleteANotFoundCategory))]
    [Trait("Application", "DeleteCategory - Use Cases")]
    public async void ShoudlThrowAnErrorWhenTryToDeleteANotFoundCategory()
    {
        var unitOfWorkRepositoryMock = _fixture.GetUnitOfWorkMockRepository();
        var categoryRepositoryMock = _fixture.GetCategoryMockRepository();
        var category = _fixture.GetCategory();
        categoryRepositoryMock.Setup(
            x => x.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )
         ).ThrowsAsync(new NotFoundException($"Category '{category.Id}' not found"));


        var sut = new UseCases.DeleteCategory(
            categoryRepositoryMock.Object,
            unitOfWorkRepositoryMock.Object
        );
        var input = new DeleteCategoryInput(category.Id);

        var action = async () => await sut.Handle(input, CancellationToken.None);

        await action.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Category '{category.Id}' not found");

        categoryRepositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkRepositoryMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        categoryRepositoryMock.Verify(x => x.DeleteAsync(category, It.IsAny<CancellationToken>()), Times.Never);
    }
}
