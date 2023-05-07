using FC.CodeFlix.Catalog.UnitTests.Application.Category.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.DeleteCategory;

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class CollectionDeleteCategoryTestFixture : ICollectionFixture<DeleteCategoryTestFixture> { }

public class DeleteCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public DeleteCategoryTestFixture() : base()
    {

    }

}
