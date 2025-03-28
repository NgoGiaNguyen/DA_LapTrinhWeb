using Microsoft.AspNetCore.Mvc;

namespace DA.Controllers
{
    public class SanPhamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
