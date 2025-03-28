using DA.DataAccess;
using DA.Models;
using DA.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var LapTop = _context.Products.Where(p => p.categoryId == 4).ToList();
            var ManHinh = _context.Products.Where(p => p.categoryId == 6).ToList();
            var BanPhim = _context.Products.Where(p => p.categoryId == 2).ToList();
            var Chuot = _context.Products.Where(p => p.categoryId == 3).ToList();
            var product = _context.Products.OrderByDescending(p=>p.id).ToList();
            var banner = _context.Banners.OrderBy(p => p.ViTri).ToList();
            TrangChu trangChu = new TrangChu();
            foreach (var item in banner)
            {
                trangChu.Banners.Add(item);
            }
            int j = 0;
            foreach (var item in product)
            {
                if (j == 4) break;
                trangChu.products.Add(item);
                j++;
            }
            j = 0;
            foreach (var item in Chuot)
            {
                if (j == 4) break;
                trangChu.Chuot.Add(item);
                j++;
            }
            j = 0;
            foreach (var item in BanPhim)
            {
                if (j == 4) break;
                trangChu.BanPhim.Add(item);
                j++;
            }
            j = 0;
            foreach (var item in ManHinh)
            {
                if (j == 4) break;
                trangChu.ManHinh.Add(item);
                j++;
            }
            j=0;
            foreach (var item in LapTop)
            {
                if (j == 4) break;
                trangChu.LapTop.Add(item);
                j++;
            }
            return View(trangChu);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
