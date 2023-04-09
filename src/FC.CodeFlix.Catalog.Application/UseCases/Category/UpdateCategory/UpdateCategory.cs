using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.Repositories;
using DomainEntity = FC.CodeFlix.Catalog.Domain.Entities;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
public sealed class UpdateCategory : IUpdateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryModelOutput> Handle(UpdateCategoryInput input, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsync(input.Id, cancellationToken);

        category.Update(input.Name, input.Description);
        CallActiveMethods(category, input);


        await _categoryRepository.UpdateAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return CategoryModelOutput.FromCategory(category);
    }

    internal static void CallActiveMethods(DomainEntity.Category category, UpdateCategoryInput updateCategoryInput)
    {

        if (category.IsActive == updateCategoryInput.IsActive || updateCategoryInput.IsActive is null) return;

        if ((bool)updateCategoryInput.IsActive)
        {
            category.Activate();
            return;
        }

        category.Deactivate();
    }
}
