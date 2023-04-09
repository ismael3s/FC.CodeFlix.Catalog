using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.Repositories;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.GetCategory;
public sealed class GetCategory : IGetCategory
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategory(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryModelOutput> Handle(GetCategoryInput input, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsync(input.Id, cancellationToken);

        return CategoryModelOutput.FromCategory(category);
    }
}
