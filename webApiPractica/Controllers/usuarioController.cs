using Microsoft.AspNetCore.Mvc;

namespace webApiPractica.Controllers
{
    public class usuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
