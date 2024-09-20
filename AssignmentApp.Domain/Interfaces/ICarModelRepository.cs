namespace Assignment.Domain.Interfaces;

public interface ICarModelRepository
{
    Task<IEnumerable<CarModel>> GetCarModelsAsync(int makerId, int year);
}
