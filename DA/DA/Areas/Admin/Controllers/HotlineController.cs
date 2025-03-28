
using DA.DataAccess;
using DA.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HotlineController : Controller
    {
        private readonly IHotlineRepository _hotlineRepository;
        public HotlineController(IHotlineRepository hotlineRepository)
        {
            _hotlineRepository = hotlineRepository;

        }
        public async Task<IActionResult> Index()
        {
            var hotlines = await _hotlineRepository.GetAllAsync();

            return View(hotlines);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var hotline = await _hotlineRepository.GetByIdAsync(id);
            await _hotlineRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
