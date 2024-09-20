namespace Assignment.Infrastructure.Repository;

public class CarMakeRepository : ICarMakeRepository
{
    private readonly ILogger<CarMakeRepository> _logger;
    private readonly IConfiguration _configuration;

    public CarMakeRepository(ILogger<CarMakeRepository> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<IEnumerable<CarMake>> GetCarMakesAsync()
    {

        try
        {
            var baseurl = _configuration.GetSection("VehiclesBaseUrl")?.Value;
            var getAllMakesUrl = $"{baseurl}/getallmakes?format=json";
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(getAllMakesUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<CarMake>(); 
                }
                var content = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<ResponseBody<CarMake>> (content);
                var allMakes = responseResult.Results.AsEnumerable();
                return allMakes;
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "an error occurred while requesting the api");
            return Enumerable.Empty<CarMake>();
        }
        catch (Exception ex)
        {

            _logger.LogError($"error: {ex.Message}");
            return Enumerable.Empty<CarMake>();
        }
    }
}
