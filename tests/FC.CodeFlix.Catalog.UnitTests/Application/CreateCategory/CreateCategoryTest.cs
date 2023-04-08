using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Domain.Entities;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Domain.Repositories;
using FluentAssertions;
using Moq;
using SUT = FC.CodeFlix.Catalog.Application.UseCases.Category;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public partial class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _createCategoryTestFixture;
    public CreateCategoryTest(CreateCategoryTestFixture createCategoryTestFixture)
    {
        _createCategoryTestFixture = createCategoryTestFixture;
    }

    [Theory(DisplayName = nameof(CreateCategory_ShouldCreateCategory_WhenValidDataIsProvide))]
    [Trait("Application", "CreateCategory - Use Cases")]
    [InlineData(true)]
    [InlineData(false)]
    public async void CreateCategory_ShouldCreateCategory_WhenValidDataIsProvide(bool isActive)
    {
        var repositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var sut = new SUT.CreateCategory.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var input = _createCategoryTestFixture.GetValidCreateCategoryInput(isActive);

        var output = await sut.Handle(input, CancellationToken.None);

        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(isActive);
        output.CreatedAt.Should().NotBeSameDateAs(default);

        repositoryMock.Verify(repository =>
            repository.InsertAsync(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
        unitOfWorkMock.Verify(uow =>
            uow.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }


    [Fact(DisplayName = nameof(CreateCategory_ShouldNotCreateCategory_WhenInvalidDataIsProvide))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory_ShouldNotCreateCategory_WhenInvalidDataIsProvide()
    {
        var repositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var sut = new SUT.CreateCategory.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var input = _createCategoryTestFixture.GetInvalidCreateCategoryInput();

        var action = async () => await sut.Handle(input, CancellationToken.None);

        await action.Should().ThrowAsync<EntityValidationException>();
        repositoryMock.Verify(repository =>
            repository.InsertAsync(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Never()
        );
        unitOfWorkMock.Verify(uow =>
            uow.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Never()
        );
    }
}
