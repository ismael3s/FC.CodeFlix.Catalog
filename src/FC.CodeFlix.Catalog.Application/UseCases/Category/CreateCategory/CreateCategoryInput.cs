using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
public record CreateCategoryInput(string Name, string? Description = null, bool IsActive = true) : IRequest<CreateCategoryOutput>
{
    public string Description { get; set; } = Description ?? "";
}
