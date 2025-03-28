using DA.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DA.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository,
        ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> AppleList()
        {
            var products = await _productRepository.GetAllApple();
            return View(products);
        }

        public async Task<IActionResult> BanPhimList()
        {
            var products = await _productRepository.GetAllBanPhim();
            return View(products);
        }

        public async Task<IActionResult> ChuotList()
        {
            var products = await _productRepository.GetAllChuot();
            return View(products);
        }

        public async Task<IActionResult> LaptopList()
        {
            var products = await _productRepository.GetAllLapTop();
            return View(products);
        }
        public async Task<IActionResult> LoaList()
        {
            var products = await _productRepository.GetAllLoa();
            return View(products);
        }

        public async Task<IActionResult> ManHinhList()
        {
            var products = await _productRepository.GetAllManHinh();
            return View(products);
        }

        public async Task<IActionResult> PCList()
        {
            var products = await _productRepository.GetAllPC();
            return View(products);
        }

        public async Task<IActionResult> TaiNgheList()
        {
            var products = await _productRepository.GetAllTaiNghe();
            return View(products);
        }

        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
