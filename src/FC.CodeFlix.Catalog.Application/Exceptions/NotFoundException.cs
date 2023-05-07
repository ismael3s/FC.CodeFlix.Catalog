namespace FC.CodeFlix.Catalog.Application.Exceptions;

[Serializable]
public class NotFoundException : ApplicationException
{
    public NotFoundException(string? message) : base(message)
    {
    }

    public static void ThrowIfNull(object? obj, string? message)
    {
        if (obj is null)
            throw new NotFoundException(message);
    }

}
