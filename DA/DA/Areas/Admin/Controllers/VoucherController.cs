using DA.Models;
using DA.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class VoucherController : Controller
    {
       

        private readonly IVoucherRepository _voucherRepository;

        public VoucherController(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<IActionResult> Index()
        {
            var voucher = await _voucherRepository.GetAllAsync();

            return View(voucher);
        }

        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Voucher voucher)
        {

            if (voucher.Id == null || voucher.Name == null || voucher.Code == null || voucher.Value == null || voucher.SoLuong == null || voucher.Value <= 0 || voucher.Value >100)
            {
                if (ModelState.IsValid)
                {
                    _voucherRepository.AddAsync(voucher);
                    return RedirectToAction("Index");
                }
                return View(voucher);
            }
            else
            {
                await _voucherRepository.AddAsync(voucher);
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> Update(int id)
        {
            var voucher = await _voucherRepository.GetByIdAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }

        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Voucher voucher)
        {
            if (id != voucher.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
             
                await _voucherRepository.UpdateAsync(voucher);
                return RedirectToAction(nameof(Index));
            }
            return View(voucher);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var voucher = await _voucherRepository.GetByIdAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }
        // Xử lý xóa sản phẩm
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _voucherRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
