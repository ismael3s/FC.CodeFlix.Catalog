using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
public record UpdateCategoryInput(Guid Id, string Name, string? Description, bool? IsActive) : IRequest<CategoryModelOutput>
{
}
