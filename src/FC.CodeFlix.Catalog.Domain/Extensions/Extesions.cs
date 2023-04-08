namespace FC.CodeFlix.Catalog.Domain.Extensions;
public static class Extesions
{
    public static string CharacteresCountIsBetween(this string @this, int min, int max, Action action)
    {
        if (@this.Length < min || @this.Length > max) action();

        return @this;
    }

    public static string CharacteresCountIsBetween(this string @this, int min, int max)
    {
        return CharacteresCountIsBetween(@this, min, max, () => throw new ArgumentException($"{@this} should character count should be between {min} and {max}"));
    }
}
