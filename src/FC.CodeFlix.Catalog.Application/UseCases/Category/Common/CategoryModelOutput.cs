using DomainEntities = FC.CodeFlix.Catalog.Domain.Entities;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
public record CategoryModelOutput(Guid Id, string Name, string Description, bool IsActive, DateTime CreatedAt)
{

    public static CategoryModelOutput FromCategory(DomainEntities.Category category)
    {
        return new CategoryModelOutput(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAt
        );
    }
}
