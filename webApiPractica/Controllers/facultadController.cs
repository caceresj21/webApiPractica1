using Microsoft.AspNetCore.Mvc;

namespace webApiPractica.Controllers
{
    public class facultadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
