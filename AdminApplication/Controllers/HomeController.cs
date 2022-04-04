using Microsoft.AspNetCore.Mvc;

namespace AdminApplication.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}