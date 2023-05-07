using FC.CodeFlix.Catalog.UnitTests.Application.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.GetCategory;

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection : ICollectionFixture<GetCategoryTestFixture> { }

public class GetCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public GetCategoryTestFixture() : base()
    {

    }


}
