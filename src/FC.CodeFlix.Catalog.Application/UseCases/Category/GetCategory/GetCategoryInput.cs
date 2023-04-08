using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.GetCategory;
public record GetCategoryInput(Guid Id) : IRequest<CategoryModelOutput>
{
}
