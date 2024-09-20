namespace Assignment.Infrastructure.Repository;

public class VehicleTypesRepository : IVehicleTypesRepository
{
    private readonly ILogger<VehicleTypesRepository> _logger;
    private readonly IConfiguration _configuration;

    public VehicleTypesRepository(ILogger<VehicleTypesRepository> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<IEnumerable<VehicleType>> GetVehicleTypesAsync(int makerId)
    {
        try
        {
            var baseurl = _configuration.GetSection("VehiclesBaseUrl")?.Value;
            var getVehicleUrl = $"{baseurl}/GetVehicleTypesForMakeId/{makerId}?format=json";
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(getVehicleUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<VehicleType>();
                }
                var content = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<ResponseBody<VehicleType>>(content);
                var allVehicleTypes = responseResult.Results.AsEnumerable();
                return allVehicleTypes;
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "an error occurred while requesting the api");
            return Enumerable.Empty<VehicleType>();
        }
        catch (Exception ex)
        {

            _logger.LogError($"error: {ex.Message}");
            return Enumerable.Empty<VehicleType>();
        }
    }
}
