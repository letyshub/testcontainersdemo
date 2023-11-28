using Microsoft.AspNetCore.Mvc;

namespace TestContainersDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarsRepository _carsRepository;
    private readonly ILogger<CarController> _logger;

    public CarController(ILogger<CarController> logger, ICarsRepository carsRepository)
    {
        _logger = logger;
        _carsRepository = carsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct)
    {
        return Ok(await _carsRepository.GetAllAsync(ct));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(await _carsRepository.GetAsync(id, ct));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] Car car, CancellationToken ct)
    {
        return Ok(await _carsRepository.AddAsync(car, ct));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromBody] Car car, [FromRoute] Guid id, CancellationToken ct)
    {
        car.Id = id;
        return Ok(await _carsRepository.UpdateAsync(car, ct));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken ct)
    {
        await _carsRepository.DeleteAsync(id, ct);
        return Ok();
    }
}
