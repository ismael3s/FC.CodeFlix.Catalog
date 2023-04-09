using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
public record DeleteCategoryInput(Guid Id) : IRequest<CategoryModelOutput>
{
}
