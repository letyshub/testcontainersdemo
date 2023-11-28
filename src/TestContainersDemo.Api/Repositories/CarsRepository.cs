using Marten;

namespace TestContainersDemo.Api;

public class CarsRepository : ICarsRepository
{
    private readonly IDocumentStore _store;

    public CarsRepository(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<Car> AddAsync(Car car, CancellationToken ct)
    {
        await using var session = _store.LightweightSession();
        session.Store(car);
        await session.SaveChangesAsync(ct);
        return car;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        await using var session = _store.LightweightSession();
        session.Delete<Car>(id);
        await session.SaveChangesAsync(ct);
    }


    public async Task<IReadOnlyList<Car>> GetAllAsync(CancellationToken ct)
    {
        await using var session = _store.QuerySession();
        return await session.Query<Car>().OrderBy(x => x.Name).ToListAsync(ct);
    }

    public async Task<Car?> GetAsync(Guid id, CancellationToken ct)
    {
        await using var session = _store.QuerySession();
        return await session.LoadAsync<Car>(id, ct);
    }

    public async Task<Car> UpdateAsync(Car car, CancellationToken ct)
    {
        await using var session = _store.LightweightSession();
        var entity = await session.LoadAsync<Car>(car.Id, ct);

        if (entity == null)
        {
            throw new Exception($"Not found car with {car.Id}");
        }

        entity.Name = car.Name;
        session.Update<Car>(entity);
        await session.SaveChangesAsync(ct);
        return car;
    }
}
