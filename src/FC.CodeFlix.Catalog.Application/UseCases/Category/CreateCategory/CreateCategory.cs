namespace FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;

using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.Repositories;
using DomainEntity = FC.CodeFlix.Catalog.Domain.Entities;
public sealed class CreateCategory : ICreateCategory
{

    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<CategoryModelOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken)
    {
        var category = DomainEntity.Category.Create(
            input.Name,
            input.Description,
            input.IsActive
        );

        await _categoryRepository.InsertAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);


        return CategoryModelOutput.FromCategory(category);
    }
}
