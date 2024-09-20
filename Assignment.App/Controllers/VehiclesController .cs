using Assignment.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net.Http;

namespace Assignment.App.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string VehiclesUrl;
        private readonly IDistributedCache _cache;

        public VehiclesController(IConfiguration configuration, IDistributedCache cache)
        {
            _configuration = configuration;
            _cache = cache;
            VehiclesUrl = _configuration.GetSection("VehiclesApiUrl").Value.ToString();
        }
        public async Task<IActionResult> Index()
        {

            return View();
        }
        public async Task<IActionResult> GetAllMakes()
        {
            var cacheKey = "GetAllMakes";
            string cachedResponse = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var makes = JsonConvert.DeserializeObject<IEnumerable<CarMake>>(cachedResponse);
                return Json(new { result = makes, errorMessage = "", isSuccess = true });
            }

            try
            {
                var _httpClient = new HttpClient();
                var response = await _httpClient.GetStringAsync($"{VehiclesUrl}/GetAllMakes");
                await _cache.SetStringAsync(cacheKey, response, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) 
                });

                var makes = JsonConvert.DeserializeObject<IEnumerable<CarMake>>(response);
                return Json(new { result = makes, errorMessage = "", isSuccess = true });
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = ex.Message, isSuccess = true });
            }
        }
       
        public async Task<IActionResult> GetVehicleTypes(int makerId)
        {
            try
            {
                var _httpClient = new HttpClient();
                var response = await _httpClient.GetStringAsync($"{VehiclesUrl}/GetVehicleTypes?makerId={makerId}");
                var types = JsonConvert.DeserializeObject<IEnumerable<VehicleType>>(response);
                return Json(new { result = types, errorMessage = "", isSuccess = true });
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = ex.Message, isSuccess = true });

            }
        }
        public async Task<IActionResult> GetCarModels(int makerId,int year)
        {

            try
            {
                var _httpClient = new HttpClient();
                var response = await _httpClient.GetStringAsync($"{VehiclesUrl}/GetCarModels?makerId={makerId}&year={year}");
                var models = JsonConvert.DeserializeObject<IEnumerable<CarModel>>(response);
                return Json(new { result = models, errorMessage = "", isSuccess = true });
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = ex.Message, isSuccess = true });

            }
        }
    }
}
