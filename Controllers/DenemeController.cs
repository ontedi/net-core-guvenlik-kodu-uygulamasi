using Microsoft.AspNetCore.Mvc;
namespace GuvenlikKoduUygulamasi.Controllers
{
    public class DenemeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
