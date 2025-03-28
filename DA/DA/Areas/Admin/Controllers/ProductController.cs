using DA.Models;
using DA.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        // Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index(int id)
        {
            var products = await _productRepository.GetAllAsync();
            if (id !=  0)
            {
                products = await _productRepository.GetAllIdAsync(id);
            }
            foreach (var product in products)
            {
                if (product.categoryId != null)
                {
                    product.category = await _categoryRepository.GetByIdAsync(product.categoryId);
                }
            }
            return View(products);
        }
        // Hiển thị form thêm sản phẩm mới
        public async Task<IActionResult> AddAsync()
        {

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "id", "name");
            return View();
        }
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile imageULR)
        {
            if (product.id == null || product.name == null || product.price == null || product.categoryId == null || imageULR == null)
            {
                if (!ModelState.IsValid)
                {

                    // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
                    var categories = await _categoryRepository.GetAllAsync();
                    ViewBag.Categories = new SelectList(categories, "id", "name");
                    return View(product);
                }
                else
                {
                    if (imageULR != null)
                    {
                        product.imageULR = await SaveImage(imageULR);
                    }
                    await _productRepository.AddAsync(product);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (imageULR != null)
                {
                    product.imageULR = await SaveImage(imageULR);
                }
                await _productRepository.AddAsync(product);
                return RedirectToAction("Index");
            }

         }

        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName; // Trả về đường dẫn tương đối
        }
        // Hiển thị thông tin chi tiết sản phẩm
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Hiển thị form cập nhật sản phẩm
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
        
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "id", "name", product.id);
            return View(product);
        }

        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Product product, IFormFile imageULR)
        {
            if (id != product.id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                if (imageULR != null)
                {
                    product.imageULR = await SaveImage(imageULR);
                }

                _productRepository.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "id", "name");
            return View(product);
        }
        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Xử lý xóa sản phẩm
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
