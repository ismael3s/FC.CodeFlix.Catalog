using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.Repositories;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
public sealed class DeleteCategory : IDeleteCategory
{

    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryModelOutput> Handle(DeleteCategoryInput input, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsync(input.Id, cancellationToken);

        await _categoryRepository.DeleteAsync(category, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return CategoryModelOutput.FromCategory(category);
    }
}
