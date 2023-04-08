using DomainEntities = FC.CodeFlix.Catalog.Domain.Entities;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
public record CreateCategoryOutput(Guid Id, string Name, string Description, bool IsActive, DateTime CreatedAt)
{

    public static CreateCategoryOutput FromCategory(DomainEntities.Category category)
    {
        return new CreateCategoryOutput(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAt
        );
    }
}
