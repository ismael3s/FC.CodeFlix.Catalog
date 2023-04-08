namespace FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
public record CreateCategoryOutput(Guid Id, string Name, string Description, bool IsActive, DateTime CreatedAt)
{
}
