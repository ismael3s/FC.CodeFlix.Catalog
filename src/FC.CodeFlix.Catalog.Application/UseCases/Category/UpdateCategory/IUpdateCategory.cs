﻿using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
public interface IUpdateCategory : IRequestHandler<UpdateCategoryInput, CategoryModelOutput>
{

    public new Task<CategoryModelOutput> Handle(UpdateCategoryInput input, CancellationToken cancellationToken);
}
