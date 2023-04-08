namespace FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
public record CreateCategoryInput(string Name, string? Description = null, bool IsActive = true)
{
    public string Description { get; set; } = Description ?? "";
}
