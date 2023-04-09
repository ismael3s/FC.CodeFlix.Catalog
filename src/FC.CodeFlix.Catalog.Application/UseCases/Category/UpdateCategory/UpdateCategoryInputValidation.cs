using FluentValidation;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
public class UpdateCategoryInputValidation : AbstractValidator<UpdateCategoryInput>
{
    public UpdateCategoryInputValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
