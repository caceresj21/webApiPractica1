using Microsoft.AspNetCore.Mvc;

namespace webApiPractica.Controllers
{
    public class reservaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
