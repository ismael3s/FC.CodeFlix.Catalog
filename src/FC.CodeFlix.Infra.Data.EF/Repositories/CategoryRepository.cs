using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Domain.Entities;
using FC.CodeFlix.Catalog.Domain.Repositories;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;
public class CategoryRepository : ICategoryRepository
{
    private readonly CodeflixCatalogDbContext _dbContext;
    private DbSet<Category> _categories => _dbContext.Categories;
    public CategoryRepository(CodeflixCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task DeleteAsync(Category aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(_categories.Remove(aggregate));
    }

    public async Task<Category> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categories.AsNoTracking().FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
        NotFoundException.ThrowIfNull(category, $"Category not found");
        return category!;
    }

    public async Task InsertAsync(Category aggregate, CancellationToken cancellationToken)
    {
        await _categories.AddAsync(aggregate, cancellationToken);
    }

    public async Task<SearchOutput<Category>> SearchAsync(SearchInput searchInput, CancellationToken cancellationToken)
    {
        var skipTo = (searchInput.Page - 1) * searchInput.PerPage;
        var categoriesQuery = _categories.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(searchInput.Search))
        {
            categoriesQuery = categoriesQuery.Where(category => category.Name.Contains(searchInput.Search, StringComparison.OrdinalIgnoreCase));
        }
        var total = await categoriesQuery.CountAsync();
        var categories = await categoriesQuery
            .AsNoTracking()
            .Skip(skipTo)
            .Take(searchInput.PerPage)
            .ToListAsync(cancellationToken);
        return new SearchOutput<Category>(
            currentPage: searchInput.Page,
            perPage: searchInput.PerPage,
            total: total,
            items: categories
        );
    }

    public Task UpdateAsync(Category aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(_categories.Update(aggregate));
    }
}
