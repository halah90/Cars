using System.Net.Http.Headers;
using System.Text.Json;
using Application.Inerfaces;
using Application.Services.CarsMake.Models;

namespace Application.Services.CarsMake;


public class CarMakeService : ICarMakeService
{
    private readonly HttpClient myClient;
    public CarMakeService(HttpClient myClient)
    {
        this.myClient = myClient;
    }

    public async Task<GetModelsResponse> GetModels(int year, string makeName)
    {
        var id = GetMakeId(makeName);

        if (id == null)
        {
            return null;
        }

        var response = await GetModelsForMakeIdYear(year, id.Value);

        if (response is null)
        {
            return null;
        }

        return new GetModelsResponse
        {
            Models = response.Results.Select(x => x.Model_Name).ToList()
        };
    }

    private static int? GetMakeId(string makeName)
    {
        string jsonData = File.ReadAllText("CarsMake.json");

        var carMakers = JsonSerializer.Deserialize<List<CarMake>>(jsonData);

        var makeId = carMakers?.Find(x => x.make_name.ToUpper() == makeName.ToUpper())?.make_id;

        return makeId;
    }

    private async Task<IntegrationResponse> GetModelsForMakeIdYear(int year, int makeId)
    {
        string endPoint = string.Format("https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{0}/modelyear/{1}?format=json", makeId, year);
        myClient.DefaultRequestHeaders.Accept.Clear();
        myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await myClient.GetAsync(endPoint);

        var stringResult = await response.Content.ReadAsStringAsync();
        var models = JsonSerializer.Deserialize<IntegrationResponse>(stringResult);

        return models;
    }
}