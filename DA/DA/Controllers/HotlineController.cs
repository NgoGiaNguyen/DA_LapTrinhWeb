using DA.DataAccess;
using DA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DA.Controllers
{


    public class HotlineController : Controller
    {


        private readonly ApplicationDbContext _context;
        public HotlineController(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Hotline hotline)
        {
          
            if (hotline.Id == null || hotline.SDT == null || hotline.Name == null || hotline.Description == null)
            {
                if (!ModelState.IsValid)
                {
                    return View(hotline);
                }

                return View(hotline);
            }
            else
            {
                _context.Hotlines.Add(hotline);
                await _context.SaveChangesAsync();
                return RedirectToAction("SendCompleted");
            }
            return View(hotline);
        }

        public async Task<IActionResult> SendCompleted()
        {
            return View();
        }
    }
}
