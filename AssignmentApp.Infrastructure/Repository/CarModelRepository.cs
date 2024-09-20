namespace Assignment.Infrastructure.Repository;

public class CarModelRepository:ICarModelRepository
{
    private readonly IConfiguration _configuration; 
    private readonly ILogger<CarModelRepository> _logger;

    public CarModelRepository(IConfiguration configuration, ILogger<CarModelRepository> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IEnumerable<CarModel>> GetCarModelsAsync(int makerId,int year)
    {

        try
        {
            var baseurl = _configuration.GetSection("VehiclesBaseUrl")?.Value;
            var getModelUrl = $"{baseurl}/GetModelsForMakeIdYear/makeId/{makerId}/modelyear/{year}?format=json";
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(getModelUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<CarModel>();
                }
                var content = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<ResponseBody<CarModel>>(content);
                var models = responseResult.Results.AsEnumerable();
                return models;
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "an error occurred while requesting the api");
            return Enumerable.Empty<CarModel>();
        }
        catch (Exception ex)
        {

            _logger.LogError($"error: {ex.Message}");
            return Enumerable.Empty<CarModel>();
        }
    }
}
