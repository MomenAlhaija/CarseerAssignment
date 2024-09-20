using Microsoft.AspNetCore.Mvc;

namespace AssignmentApp.Controllers
{
    public class VehiclesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
