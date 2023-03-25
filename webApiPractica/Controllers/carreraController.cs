using Microsoft.AspNetCore.Mvc;

namespace webApiPractica.Controllers
{
    public class carreraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
