namespace Assignment.Domain.Interfaces;

public interface IVehicleTypesRepository
{
    Task<IEnumerable<VehicleType>> GetVehicleTypesAsync(int makerId);  
}
