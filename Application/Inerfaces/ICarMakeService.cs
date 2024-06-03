using Application.Services.CarsMake.Models;

namespace Application.Inerfaces;

public interface ICarMakeService
{
    Task<GetModelsResponse> GetModels(int year, string makeName);
}