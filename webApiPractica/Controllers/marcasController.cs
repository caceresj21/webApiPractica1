using Microsoft.AspNetCore.Mvc;

namespace webApiPractica.Controllers
{
    public class marcasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
