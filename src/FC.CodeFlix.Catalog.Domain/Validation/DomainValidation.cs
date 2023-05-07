using FC.CodeFlix.Catalog.Domain.Exceptions;

namespace FC.CodeFlix.Catalog.Domain.Validation;
public class DomainValidation
{
    protected DomainValidation()
    {
    }

    public static void IsNotNull(object target, string fieldName)
    {
        if (target is not null) return;

        throw new EntityValidationException($"{fieldName} should not be null");
    }

    public static void IsNotNullOrEmpty(string target, string fieldName)
    {
        if (!string.IsNullOrWhiteSpace(target)) return;

        throw new EntityValidationException($"{fieldName} should not be null or empty");

    }

    public static void MinLength(string value, int minLength, string fieldName)
    {
        if (value is null)
        {
            throw new EntityValidationException($"{fieldName} should not be less than {minLength} characteres");
        }

        if (value.Length >= minLength) return;

        throw new EntityValidationException($"{fieldName} should not be less than {minLength} characteres");
    }

    public static void MaxLength(string value, int maxLength, string fieldName)
    {

        if (value is null) throw new EntityValidationException($"{fieldName} should not be greather than {maxLength} characteres");

        if (value?.Length <= maxLength) return;

        throw new EntityValidationException($"{fieldName} should not be greather than {maxLength} characteres");
    }

    public static void StringHasValue(string value, string variableName)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw new EntityValidationException($"{variableName} should not be empty or null");
        }
    }
}