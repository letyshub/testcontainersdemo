namespace TestContainersDemo.Api;

public interface ICarsRepository
{
    Task<Car> AddAsync(Car car, CancellationToken ct);
    Task<Car> UpdateAsync(Car car, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task<Car?> GetAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Car>> GetAllAsync(CancellationToken ct);
}