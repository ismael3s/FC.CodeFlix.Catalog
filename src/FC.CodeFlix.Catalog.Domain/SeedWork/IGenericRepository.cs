namespace FC.CodeFlix.Catalog.Domain.SeedWork;
public interface IGenericRepository<TAggregate> : IRepository
{
    public Task InsertAsync(TAggregate aggregate, CancellationToken cancellationToken);
    public Task<TAggregate> GetAsync(Guid id, CancellationToken cancellationToken);
}
