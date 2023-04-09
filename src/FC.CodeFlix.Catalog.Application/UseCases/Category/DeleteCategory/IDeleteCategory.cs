using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
public interface IDeleteCategory : IRequestHandler<DeleteCategoryInput, CategoryModelOutput>
{
    public new Task<CategoryModelOutput> Handle(DeleteCategoryInput input, CancellationToken cancellationToken);
}
