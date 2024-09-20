using Microsoft.AspNetCore.Mvc;

namespace Assignment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly ICarMakeRepository _carMakeRepository;
    private readonly IVehicleTypesRepository _vehicleTypesRepository;
    private readonly ICarModelRepository _carModelRepository;
    public VehiclesController(ICarMakeRepository carMakeRepository, IVehicleTypesRepository vehicleTypesRepository, ICarModelRepository carModelRepository)
    {
        _carMakeRepository = carMakeRepository;
        _vehicleTypesRepository = vehicleTypesRepository;
        _carModelRepository = carModelRepository;
    }

    [HttpGet("GetAllMakes")]
    public async Task<IActionResult> GetAllMakes()
    {
        var makes=await _carMakeRepository.GetCarMakesAsync();    
        return Ok(makes);   
    }
    [HttpGet("GetVehicleTypes")]
    public async Task<IActionResult> GetVehicleTypes(int makerId)
    {
        var vehicleTypes = await _vehicleTypesRepository.GetVehicleTypesAsync(makerId);
        return Ok(vehicleTypes);
    }
    [HttpGet("GetCarModels")]
    public async Task<IActionResult> GetCarModels(int makerId,int year)
    {
        var models=await _carModelRepository.GetCarModelsAsync(makerId, year);   
        return Ok(models);
    }
}
