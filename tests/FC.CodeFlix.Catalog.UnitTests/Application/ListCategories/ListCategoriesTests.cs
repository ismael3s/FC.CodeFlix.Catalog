using FC.CodeFlix.Catalog.Domain.Entities;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using Moq;
using UseCase = FC.CodeFlix.Catalog.Application.UseCases.Category.ListCategories;

namespace FC.CodeFlix.Catalog.UnitTests.Application.ListCategories;
[Collection(nameof(ListCategoriesTestsFixture))]
public class ListCategoriesTests
{
    private readonly ListCategoriesTestsFixture _fixture;

    public ListCategoriesTests(ListCategoriesTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(List))]
    [Trait("Application", "List Categories - Use Cases")]
    public async Task List()
    {
        var repositoryMock = _fixture.GetCategoryRepositoryMock();
        var input = _fixture.GetInput();
        var categories = _fixture.GetCategories().ToList();
        var outputRepositorySearch = new SearchOutput<Category>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: (IReadOnlyList<Category>)categories,
            total: (new Random()).Next(10, 100)
        );
        repositoryMock.Setup(x => x.SearchAsync(
            It.Is<SearchInput>(searchInput =>
                searchInput.Page == input.Page &&
                searchInput.PerPage == input.PerPage &&
                searchInput.Search == input.Search &&
                searchInput.OrderBy == input.OrderBy &&
                searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new UseCase.ListCategories(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().NotBeNull();
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        output.Items.Should().BeEquivalentTo(outputRepositorySearch.Items);
    }


    [Fact(DisplayName = nameof(ListShouldReturnAnEmptyList))]
    [Trait("Application", "List Categories - Use Cases")]
    public async Task ListShouldReturnAnEmptyList()
    {
        var repositoryMock = _fixture.GetCategoryRepositoryMock();
        var input = _fixture.GetInput();
        var outputRepositorySearch = new SearchOutput<Category>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: (new List<Category>()).AsReadOnly(),
            total: 0
        );
        repositoryMock.Setup(x => x.SearchAsync(
            It.Is<SearchInput>(searchInput =>
                searchInput.Page == input.Page &&
                searchInput.PerPage == input.PerPage &&
                searchInput.Search == input.Search &&
                searchInput.OrderBy == input.OrderBy &&
                searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new UseCase.ListCategories(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(0);
        output.Items.Should().NotBeNull();
        output.Items.Should().HaveCount(0);
        output.Items.Should().BeEquivalentTo(outputRepositorySearch.Items);
    }

    [Theory(DisplayName = nameof(ListWithoutAllInputParams))]
    [Trait("Application", "List Categories - Use Cases")]
    [MemberData(
        nameof(ListCategoriesTestDataGenerator.GetInputWithoutAllParams),
        parameters: 14,
        MemberType = typeof(ListCategoriesTestDataGenerator)
     )]
    public async Task ListWithoutAllInputParams(UseCase.ListCategoriesInput input)
    {
        var repositoryMock = _fixture.GetCategoryRepositoryMock();
        var categories = _fixture.GetCategories().ToList();
        var outputRepositorySearch = new SearchOutput<Category>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: (IReadOnlyList<Category>)categories,
            total: (new Random()).Next(10, 100)
        );
        repositoryMock.Setup(x => x.SearchAsync(
            It.Is<SearchInput>(searchInput =>
                searchInput.Page == input.Page &&
                searchInput.PerPage == input.PerPage &&
                searchInput.Search == input.Search &&
                searchInput.OrderBy == input.OrderBy &&
                searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new UseCase.ListCategories(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().NotBeNull();
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        output.Items.Should().BeEquivalentTo(outputRepositorySearch.Items);
    }
}
