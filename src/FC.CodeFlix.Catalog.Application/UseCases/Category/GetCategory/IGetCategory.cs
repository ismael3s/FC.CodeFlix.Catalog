using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;
namespace FC.CodeFlix.Catalog.Application.UseCases.Category.GetCategory;
public interface IGetCategory : IRequestHandler<GetCategoryInput, CategoryModelOutput>
{
    public new Task<CategoryModelOutput> Handle(GetCategoryInput input, CancellationToken cancellationToken);
}
