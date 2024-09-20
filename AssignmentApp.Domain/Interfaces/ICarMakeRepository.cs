namespace Assignment.Domain.Interfaces;

public interface ICarMakeRepository
{
    Task<IEnumerable<CarMake>> GetCarMakesAsync();
    
}
