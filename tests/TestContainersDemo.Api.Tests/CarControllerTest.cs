using System.Net.Http.Json;

namespace TestContainersDemo.Api.Tests;

public class CarControllerTest : IClassFixture<IntegrationTestFactory>
{
    private readonly IntegrationTestFactory _factory;

    public CarControllerTest(IntegrationTestFactory factory) => _factory = factory;

    [Fact]
    public async Task Should_Make_All_Car_CRUD_Operations()
    {
        var car = new Car { Id = new Guid("8e004033-618c-4136-8997-ea82eb9786e4"), Name = "Test car" };
        var client = _factory.CreateClient();
        var rs = await client.PostAsJsonAsync("/car", car);
        Assert.Equal(System.Net.HttpStatusCode.OK, rs.StatusCode);

        car.Name = "Test car 2";
        rs = await client.PutAsJsonAsync($"car/{car.Id}", car);
        Assert.Equal(System.Net.HttpStatusCode.OK, rs.StatusCode);

        rs = await client.GetAsync($"car/{car.Id}");
        Assert.Equal(System.Net.HttpStatusCode.OK, rs.StatusCode);
        var rsCar = await rs.Content.ReadFromJsonAsync<Car>();
        Assert.NotNull(rsCar);
        Assert.Equal(rsCar.Id, car.Id);
        Assert.Equal(rsCar.Name, car.Name);

        rs = await client.GetAsync("car");
        Assert.Equal(System.Net.HttpStatusCode.OK, rs.StatusCode);
        var rsCars = await rs.Content.ReadFromJsonAsync<IList<Car>>();
        Assert.NotNull(rsCars);
        Assert.Single(rsCars);
        Assert.Equal(rsCars[0].Id, car.Id);
        Assert.Equal(rsCars[0].Name, car.Name);

        rs = await client.DeleteAsync($"car/{car.Id}");
        Assert.Equal(System.Net.HttpStatusCode.OK, rs.StatusCode);

        rs = await client.GetAsync("car");
        Assert.Equal(System.Net.HttpStatusCode.OK, rs.StatusCode);
        rsCars = await rs.Content.ReadFromJsonAsync<IList<Car>>();
        Assert.NotNull(rsCars);
        Assert.Empty(rsCars);
    }
}