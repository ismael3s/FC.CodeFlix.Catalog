namespace FC.CodeFlix.Catalog.Application.Exceptions;

[Serializable]
public class NotFoundException : ApplicationException
{
    public NotFoundException(string? message) : base(message)
    {
    }


}
