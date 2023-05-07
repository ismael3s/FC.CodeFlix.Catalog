using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.Repositories;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.ListCategories;
public class ListCategories : IListCategories
{
    private readonly ICategoryRepository _categoryRepository;

    public ListCategories(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request, CancellationToken cancellationToken)
    {
        var searchOutput = await _categoryRepository.SearchAsync(
            new(
                page: request.Page,
                perPage: request.PerPage,
                orderBy: request.OrderBy,
                order: request.Dir,
                search: request.Search
            ),
            cancellationToken);
        return new ListCategoriesOutput(
             page: searchOutput.CurrentPage,
             perPage: searchOutput.PerPage,
             total: searchOutput.Total,
             items: searchOutput.Items
                 .Select(CategoryModelOutput.FromCategory)
                 .ToList()
         );
    }
}

