using FC.CodeFlix.Catalog.UnitTests.Application.Category.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.UpdateCategory;

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
