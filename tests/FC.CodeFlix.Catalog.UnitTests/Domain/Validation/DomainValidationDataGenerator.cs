using Bogus;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Validation;
public class DomainValidationDataGenerator
{

    public static IEnumerable<object[]> MinLengthTestThrowParams(int numberOfTests = 6)
    {
        var faker = new Faker();
        for (int i = 0; i < (numberOfTests - 1); i++)
        {
            var productName = faker.Commerce.ProductName();
            yield return new object[] { productName, productName.Length + 1 };
        }

        yield return new object[] { null!, 3 };

    }

    public static IEnumerable<object[]> MinLengthTestNotThrowParams(int numberOfTests = 6)
    {

        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var productName = faker.Commerce.ProductName();
            var minLength = new Random().Next(0, productName.Length - 1);
            yield return new object[] { productName, minLength };
        }

    }

    public static IEnumerable<object[]> MaxLengthTestNotThrowParams(int numberOfTests = 6)
    {

        var faker = new Faker();
        for (int i = 0; i < (numberOfTests - 1); i++)
        {
            var productName = faker.Commerce.ProductName();
            var plus = new Random().Next(productName.Length + 1, productName.Length + 120);
            var maxLength = new Random().Next(productName.Length, plus);
            yield return new object[] { productName, maxLength };
        }

        yield return new object[] { "123", 3 };

    }

    public static IEnumerable<object[]> MaxLengthTestThrowParams(int numberOfTests = 6)
    {

        var faker = new Faker();
        for (int i = 0; i < (numberOfTests - 1); i++)
        {
            var productName = faker.Commerce.ProductName();
            var maxLength = new Random().Next(0, productName.Length - 1);
            yield return new object[] { productName, maxLength };
        }


        yield return new object[] { null!, 0 };

    }

}
