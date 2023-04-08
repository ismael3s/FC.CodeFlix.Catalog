using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
public record CreateCategoryInput(string Name, string? Description = null, bool IsActive = true) : IRequest<CategoryModelOutput>
{
    public string Description { get; set; } = Description ?? "";
}
