using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Domain.Repositories;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Moq;
using Entities = FC.CodeFlix.Catalog.Domain.Entities;
namespace FC.CodeFlix.Catalog.UnitTests.Application.Category.Common;

public abstract class CategoryUseCasesBaseFixture : BaseFixture
{
    public string GetValidCategoryName()
    {
        string name;

        do
        {
            name = Faker.Commerce.ProductName();
        } while (name?.Length < 3 || name?.Length > 255);

        return name!;
    }

    public string GetValidDescription()
    {
        var description = Faker.Commerce.ProductDescription();

        if (description.Length > 10_000) return description[..10_000];

        return description;
    }

    public bool GetRandomIsActive() => new Random().NextDouble() < 0.5;


    public Entities.Category GetCategory()
    {
        return Entities.Category.Create(GetValidCategoryName(), GetValidDescription(), GetRandomIsActive());
    }

    public IEnumerable<Entities.Category> GetCategories(int amount = 5)
    {
        for (int index = 0; index < amount; index++)
        {
            yield return GetCategory();
        }
    }


    #region Repository Mock
    public Mock<IUnitOfWork> GetUOWRepositoryMock() => new();
    public Mock<ICategoryRepository> GetCategoryRepositoryMock() => new();
    #endregion

}

