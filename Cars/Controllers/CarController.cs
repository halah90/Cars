using Application.Inerfaces;
using Application.Services.CarsMake.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers;

[Route("api/models")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarMakeService carMakeService;
    public CarController(ICarMakeService carMakeService)
    {
        this.carMakeService = carMakeService;
    }

    [HttpGet("{modelYear}/{make}")]
    [ProducesResponseType(typeof(GetModelsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int modelYear, string make)
    {
        var result = await carMakeService.GetModels(modelYear, make);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}