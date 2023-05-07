using FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Entities = FC.CodeFlix.Catalog.Domain.Entities;
using SUT = FC.CodeFlix.Catalog.Application.UseCases.Category;

namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _createCategoryTestFixture;
    public CreateCategoryTest(CreateCategoryTestFixture createCategoryTestFixture)
    {
        _createCategoryTestFixture = createCategoryTestFixture;
    }

    [Theory(DisplayName = nameof(CreateCategory_ShouldCreateCategory_WhenValidDataIsProvide))]
    [Trait("Application", "CreateCategory - Use Cases")]
    [MemberData(
        nameof(CreateCategoryDataGenerator.GetValidInputParams),
        MemberType = typeof(CreateCategoryDataGenerator)
        )]
    public async void CreateCategory_ShouldCreateCategory_WhenValidDataIsProvide(CreateCategoryInput input)
    {
        var repositoryMock = _createCategoryTestFixture.GetCategoryRepositoryMock();
        var unitOfWorkMock = _createCategoryTestFixture.GetUOWRepositoryMock();
        var sut = new SUT.CreateCategory.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var output = await sut.Handle(input, CancellationToken.None);

        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.CreatedAt.Should().NotBeSameDateAs(default);

        repositoryMock.Verify(repository =>
            repository.InsertAsync(
                It.IsAny<Entities.Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
        unitOfWorkMock.Verify(uow =>
            uow.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }


    [Theory(DisplayName = nameof(CreateCategory_ShouldNotCreateCategory_WhenInvalidDataIsProvide))]
    [Trait("Application", "CreateCategory - Use Cases")]
    [MemberData(
        nameof(CreateCategoryDataGenerator.GetInvalidInputs),
        MemberType = typeof(CreateCategoryDataGenerator)
        )]
    public async void CreateCategory_ShouldNotCreateCategory_WhenInvalidDataIsProvide(CreateCategoryInput input, string errorMessage)
    {
        var repositoryMock = _createCategoryTestFixture.GetCategoryRepositoryMock();
        var unitOfWorkMock = _createCategoryTestFixture.GetUOWRepositoryMock();
        var sut = new SUT.CreateCategory.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);


        var action = async () => await sut.Handle(input, CancellationToken.None);

        await action.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(errorMessage);


        repositoryMock.Verify(repository =>
            repository.InsertAsync(
                It.IsAny<Entities.Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Never()
        );
        unitOfWorkMock.Verify(uow =>
            uow.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Never()
        );
    }

    [Fact(DisplayName = nameof(CreateCategory_ShouldCreateCategory_OnlyWithName))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory_ShouldCreateCategory_OnlyWithName()
    {
        var repositoryMock = _createCategoryTestFixture.GetCategoryRepositoryMock();
        var unitOfWorkMock = _createCategoryTestFixture.GetUOWRepositoryMock();
        var sut = new SUT.CreateCategory.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);


        var input = new CreateCategoryInput(_createCategoryTestFixture.GetValidCategoryName());

        var output = await sut.Handle(input, CancellationToken.None);

        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(string.Empty);
        output.IsActive.Should().BeTrue();


        repositoryMock.Verify(repository =>
            repository.InsertAsync(
                It.IsAny<Entities.Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once()
        );
        unitOfWorkMock.Verify(uow =>
            uow.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Once()
        );
    }
}
