using FC.CodeFlix.Catalog.UnitTests.Application.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class CollectionUpdateCategoryTestFixture : ICollectionFixture<UpdateCategoryTestFixture>
{
}
public class UpdateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public UpdateCategoryTestFixture() : base()
    {

    }
}
