using FC.CodeFlix.Catalog.UnitTests.Application.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.GetCategory;

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class CollectionDeleteCategoryTestFixture : ICollectionFixture<DeleteCategoryTestFixture> { }

public class DeleteCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public DeleteCategoryTestFixture() : base()
    {

    }

}
