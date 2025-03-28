using DA.Models;
using DA.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace DA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BannerController : Controller
    {
        

        private readonly IBannerRepository _bannerRepository;

        public BannerController(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }
        public async Task<IActionResult> Index()
        {
            var banner = await _bannerRepository.GetAllAsync();
           
            return View(banner);
        }

        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        // Xử lý thêm sản phẩm mới

        [HttpPost]
        public async Task<IActionResult> Add(Banner banner, IFormFile ImageURL)
        {
            if (banner.Id == null || banner.ProductId == null || banner.Name == null || banner.ViTri == null || ImageURL == null)
            {
                if (ModelState.IsValid)
                {
                    if (ImageURL != null)
                    {
                        banner.ImageURL = await SaveImage(ImageURL);
                    }
                    await _bannerRepository.AddAsync(banner);
                    return RedirectToAction("Index");
                }

                return View(banner);
            }
            else
            {
                if (ImageURL != null)
                {
                    banner.ImageURL = await SaveImage(ImageURL);
                }
                await _bannerRepository.AddAsync(banner);
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

        public async Task<IActionResult> Update(int id)
        {
            var banner = await _bannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Banner banner, IFormFile ImageURL)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }


            if (banner.Id == null || banner.ProductId == null || banner.Name == null || banner.ViTri == null || ImageURL == null)
            {
                if (ModelState.IsValid)
                {
                    if (ImageURL != null)
                    {
                        banner.ImageURL = await SaveImage(ImageURL);
                    }

                    await _bannerRepository.UpdateAsync(banner);
                    return RedirectToAction(nameof(Index));
                }
                return View(banner);

            }
            else
            {
                if (ImageURL != null)
                {
                    banner.ImageURL = await SaveImage(ImageURL);
                }

                await _bannerRepository.UpdateAsync(banner);
                return RedirectToAction(nameof(Index));
            }
           
        }

        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _bannerRepository.GetByIdAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }
        // Xử lý xóa sản phẩm
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bannerRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
