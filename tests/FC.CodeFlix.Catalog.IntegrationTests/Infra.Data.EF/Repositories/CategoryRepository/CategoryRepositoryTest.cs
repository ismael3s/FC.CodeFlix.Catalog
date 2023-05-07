using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Domain.Entities;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository;
[Collection(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryTest
{
    private readonly CategoryRepositoryTestFixture _fixture;

    public CategoryRepositoryTest(CategoryRepositoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ShouldBeAbleToInsertAnCategory))]
    [Trait("Integration/Infra.Data", "CategoryRepository  - Repositories")]
    public async Task ShouldBeAbleToInsertAnCategory()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetCategory();
        var sut = _fixture.GetSUT(dbContext);

        await sut.InsertAsync(exampleCategory, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var dbCategory = await (_fixture.CreateDbContext(true)).Categories.FindAsync(exampleCategory.Id) ?? null!;
        dbCategory.Should().NotBeNull();
        dbCategory.Id.Should().Be(exampleCategory.Id);
        dbCategory.Name.Should().Be(exampleCategory.Name);
        dbCategory.Description.Should().Be(
            exampleCategory.Description
        );
        dbCategory.IsActive.Should().Be(exampleCategory.IsActive);
        dbCategory.CreatedAt.Should().Be(exampleCategory.CreatedAt);
    }

    [Fact(DisplayName = nameof(ShouldFindAnCategoryFromDatabase))]
    [Trait("Integration/Infra.Data", "CategoryRepository  - Repositories")]
    public async Task ShouldFindAnCategoryFromDatabase()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetCategory();
        await dbContext.Categories.AddAsync(exampleCategory);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var sut = _fixture.GetSUT(_fixture.CreateDbContext(true));

        var category = await sut.GetAsync(exampleCategory.Id, CancellationToken.None);

        category.Should().NotBeNull();
        category.Id.Should().Be(exampleCategory.Id);
        category.Name.Should().Be(exampleCategory.Name);
        category.Description.Should().Be(
            exampleCategory.Description
        );
        category.IsActive.Should().Be(exampleCategory.IsActive);
        category.CreatedAt.Should().Be(exampleCategory.CreatedAt);
    }

    [Fact(DisplayName = nameof(ShouldThrowErrorWhenNoCategoryIsFound))]
    [Trait("Integration/Infra.Data", "CategoryRepository  - Repositories")]
    public async Task ShouldThrowErrorWhenNoCategoryIsFound()
    {
        var dbContext = _fixture.CreateDbContext();
        var sut = _fixture.GetSUT(dbContext);

        var action = async () => await sut.GetAsync(Guid.NewGuid(), CancellationToken.None);

        await action.Should().ThrowAsync<NotFoundException>().WithMessage("Category not found");
    }

    [Fact(DisplayName = nameof(RemoveAsync_ShouldBeAbleToRemove))]
    [Trait("Integration/Infra.Data", "CategoryRepository  - Repositories")]
    public async Task RemoveAsync_ShouldBeAbleToRemove()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetCategory();
        await dbContext.Categories.AddAsync(exampleCategory);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var sut = _fixture.GetSUT(dbContext);

        await sut.DeleteAsync(exampleCategory, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);


        var category = await dbContext.Categories.FindAsync(exampleCategory.Id);
        category.Should().BeNull();
    }

    [Fact(DisplayName = nameof(Update_ShouldBeAbleToUpdateInformations))]
    [Trait("Integration/Infra.Data", "CategoryRepository  - Repositories")]
    public async Task Update_ShouldBeAbleToUpdateInformations()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetCategory();
        var name = _fixture.GenerateValidCategoryNameDifferentFrom(exampleCategory.Name);
        await dbContext.Categories.AddAsync(exampleCategory);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var sut = _fixture.GetSUT(dbContext);

        exampleCategory.Update(name);
        await sut.UpdateAsync(exampleCategory, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);


        var category = await (_fixture.CreateDbContext(true)).Categories.FindAsync(exampleCategory.Id);

        category.Should().NotBeNull();
        category!.Id.Should().Be(exampleCategory.Id);
        category.Name.Should().Be(name);
        category.Description.Should().Be(
            exampleCategory.Description
        );
        category.IsActive.Should().Be(exampleCategory.IsActive);
    }


    [Fact(DisplayName = nameof(Search_ShouldBeAbleToSearchWithParamsAndReturnListAndTotal))]
    [Trait("Integration/Infra.Data", "CategoryRepository  - Repositories")]
    public async Task Search_ShouldBeAbleToSearchWithParamsAndReturnListAndTotal()
    {
        var dbContext = _fixture.CreateDbContext();
        var categories = _fixture.GetCategories(20).ToList();
        await dbContext.Categories.AddRangeAsync(categories);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var sut = _fixture.GetSUT(dbContext);

        var searchInput = new SearchInput(
            page: 1,
            perPage: 30,
            search: "",
            orderBy: "",
            order: SearchOrder.Asc
        );
        var searchOutput = await sut.SearchAsync(searchInput, CancellationToken.None);

        searchOutput.Should().NotBeNull();
        searchOutput.Total.Should().Be(categories.Count);
        searchOutput.Items.Should().HaveCount(categories.Count);
        searchInput.Page.Should().Be(searchInput.Page);
        searchInput.PerPage.Should().Be(searchInput.PerPage);

        foreach (Category item in searchOutput.Items)
        {
            var category = categories.FirstOrDefault(x => x.Id == item.Id);
            category.Should().NotBeNull();
            category!.Id.Should().Be(item.Id);
            category.Name.Should().Be(item.Name);
            category.Description.Should().Be(
                item.Description
            );
            category.IsActive.Should().Be(item.IsActive);
            category.CreatedAt.Should().Be(item.CreatedAt);
        }
    }

    [Fact(DisplayName = nameof(Search_ShouldReturnEmptyWithoutErrors))]
    [Trait("Integration/Infra.Data", "CategoryRepository  - Repositories")]
    public async Task Search_ShouldReturnEmptyWithoutErrors()
    {
        var dbContext = _fixture.CreateDbContext();
        var sut = _fixture.GetSUT(dbContext);

        var searchInput = new SearchInput(
            page: 1,
            perPage: 30,
            search: "",
            orderBy: "",
            order: SearchOrder.Asc
        );
        var searchOutput = await sut.SearchAsync(searchInput, CancellationToken.None);

        searchOutput.Should().NotBeNull();
        searchOutput.Total.Should().Be(0);
        searchOutput.Items.Should().HaveCount(0);
        searchInput.Page.Should().Be(searchInput.Page);
        searchInput.PerPage.Should().Be(searchInput.PerPage);
    }

    [Theory(DisplayName = nameof(Search_ShouldBeAbleToSearchWithPagination))]
    [Trait("Integration/Infra.Data", "CategoryRepository  - Repositories")]
    [InlineData(10, 1, 5, 5)]
    [InlineData(7, 2, 5, 2)]
    [InlineData(7, 3, 5, 0)]
    public async Task Search_ShouldBeAbleToSearchWithPagination(
        int quanityOfCategoriesToGenerate,
        int page,
        int perPage,
        int expectedQuantityItems
    )
    {
        var dbContext = _fixture.CreateDbContext();
        var categories = _fixture.GetCategories(quanityOfCategoriesToGenerate).ToList();
        await dbContext.Categories.AddRangeAsync(categories);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var sut = _fixture.GetSUT(dbContext);

        var searchInput = new SearchInput(
            page: page,
            perPage: perPage,
            search: "",
            orderBy: "",
            order: SearchOrder.Asc
        );
        var searchOutput = await sut.SearchAsync(searchInput, CancellationToken.None);

        searchOutput.Should().NotBeNull();
        searchOutput.Total.Should().Be(quanityOfCategoriesToGenerate);
        searchOutput.Items.Should().HaveCount(expectedQuantityItems);
        searchInput.Page.Should().Be(searchInput.Page);
        searchInput.PerPage.Should().Be(searchInput.PerPage);

        foreach (Category item in searchOutput.Items)
        {
            var category = categories.FirstOrDefault(x => x.Id == item.Id);
            category.Should().NotBeNull();
            category!.Id.Should().Be(item.Id);
            category.Name.Should().Be(item.Name);
            category.Description.Should().Be(
                item.Description
            );
            category.IsActive.Should().Be(item.IsActive);
            category.CreatedAt.Should().Be(item.CreatedAt);
        }
    }

    [Theory(DisplayName = nameof(Search_ShouldBeAbleToSeachWithText))]
    [Trait("Integration/Infra.Data", "CategoryRepository  - Repositories")]
    [InlineData("Action", 1, 5, 1, 1)]
    [InlineData("Horror", 1, 5, 2, 2)]
    [InlineData("SCi", 1, 5, 4, 4)]
    [InlineData("SCi", 1, 2, 2, 4)]
    [InlineData("AOpa", 1, 10, 0, 0)]
    [InlineData("Isekai", 1, 10, 1, 1)]
    [InlineData("", 1, 5, 5, 9)]
    public async Task Search_ShouldBeAbleToSeachWithText(
       string search,
       int page,
       int perPage,
       int expectedQuantityItemsReturned,
      int expectedQuantityItemsTotal
   )
    {
        var dbContext = _fixture.CreateDbContext();
        var categories = _fixture.GetCategoriesWithNames(new List<string> { "Action", "Horror", "Horror - Based on Real Facts", "Drama", "Sci-fia IA", "Sci-fia Space", "Sci-fia Robots", "Sci-fia Future", "Anime Isekai" });

        await dbContext.Categories.AddRangeAsync(categories);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var sut = _fixture.GetSUT(dbContext);

        var searchInput = new SearchInput(
            page: page,
            perPage: perPage,
            search: search,
            orderBy: "",
            order: SearchOrder.Asc
        );
        var searchOutput = await sut.SearchAsync(searchInput, CancellationToken.None);

        searchOutput.Should().NotBeNull();
        searchOutput.Total.Should().Be(expectedQuantityItemsTotal);
        searchOutput.Items.Should().HaveCount(expectedQuantityItemsReturned);
        searchInput.Page.Should().Be(searchInput.Page);
        searchInput.PerPage.Should().Be(searchInput.PerPage);

        foreach (Category item in searchOutput.Items)
        {
            var category = categories.FirstOrDefault(x => x.Id == item.Id);
            category.Should().NotBeNull();
            category!.Id.Should().Be(item.Id);
            category.Name.Should().Be(item.Name);
            category.Description.Should().Be(
                item.Description
            );
            category.IsActive.Should().Be(item.IsActive);
            category.CreatedAt.Should().Be(item.CreatedAt);
        }
    }

}

