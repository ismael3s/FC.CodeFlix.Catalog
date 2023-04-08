using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
public interface ICreateCategory : IRequestHandler<CreateCategoryInput, CategoryModelOutput>
{
    // TODO: Validate if this new is realy needed
    public new Task<CategoryModelOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken);
}
