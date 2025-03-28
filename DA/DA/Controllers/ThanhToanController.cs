using DA.DataAccess;
using DA.Extension;
using DA.Models;
using DA.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace DA.Controllers
{
    public class ThanhToanController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ThanhToanController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            if (cart == null || cart.Items.Count < 1) 
            {   
                return RedirectToAction("Index", "GioHang");
            }
            return View(cart);
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(ShoppingCart shoppingCart)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            if (cart == null || cart.Items.Count < 1)
            {
                return RedirectToAction("Index", "GioHang");
            }
            var user = await _userManager.GetUserAsync(User);
            shoppingCart.Order.UserId = user.Id;
            shoppingCart.Order.OrderDate = DateTime.UtcNow;
            if (shoppingCart.Order.Code != null)
            {
                var voucher = _context.Vouchers.FirstOrDefault(p => p.Code == shoppingCart.Order.Code);
                if (voucher != null && voucher.SoLuong > 0)
                {
                    shoppingCart.Order.voucher = voucher;                 
                    _context.SaveChanges();
                }
            }
            if (shoppingCart.Order.voucher != null)
            {
                shoppingCart.Order.TotalPrice = cart.Items.Sum(p => p.Price * p.Quantity - (p.Price * p.Quantity * shoppingCart.Order.voucher.Value)/100);
            }
            else
            {
                shoppingCart.Order.TotalPrice = cart.Items.Sum(p => p.Price * p.Quantity);
            }
           
            shoppingCart.Order.Status = true;
            shoppingCart.Order.OrderDetails = cart.Items.Select(p => new OrderDetail
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                Price = p.Price
            }).ToList();
            _context.Orders.Add(shoppingCart.Order);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Cart");
            return RedirectToAction("OrderCompleted", new { orderId = shoppingCart.Order.Id });
        }

        public IActionResult OrderCompleted(int orderId)
        {
            var order =  _context.Orders.FirstOrDefault(p => p.Id == orderId);
            if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }

        public async Task<IActionResult> OrderList()
        {
            var user = await _userManager.GetUserAsync(User);
            var orders = await _context.Orders.Where(p => p.UserId == user.Id && p.Status == true).ToListAsync();
            ThongKe thongKe = new ThongKe();

          
            foreach (var item in orders)
            {
                thongKe.Orders.Add(item);
                thongKe.TongOrders++;
                thongKe.TotalPrice += item.TotalPrice;
            }
            return View(thongKe);
        }

        public async Task<IActionResult> Display(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(p => p.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDetails = _context.OrderDetails.Where(p => p.OrderId == id).ToList();
            foreach (var item in order.OrderDetails)
            {
                if (item.ProductId != null)
                {
                    item.Product = await _context.Products.FirstOrDefaultAsync(p => p.id == item.ProductId);
                }
            }
            return View(order);
           
        }

        public async Task<IActionResult> Hide(int id)
        {
            var orders = await _context.Orders.SingleOrDefaultAsync(p => p.Id == id);
            if (orders == null)
            {
                return NotFound();
            }
            var orderDetails = _context.OrderDetails.Where(p => p.OrderId == id).ToList();
            foreach (var item in orders.OrderDetails)
            {
                if (item.ProductId != null)
                {
                    item.Product = await _context.Products.FirstOrDefaultAsync(p => p.id == item.ProductId);
                }
            }
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> HideConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            orders.Status = false;  
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderList");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var orders = await _context.Orders.SingleOrDefaultAsync(p => p.Id == id);
            if (orders == null)
            {
                return NotFound();
            }
            var orderDetails = _context.OrderDetails.Where(p => p.OrderId == id).ToList();
            foreach (var item in orders.OrderDetails)
            {
                if (item.ProductId != null)
                {
                    item.Product = await _context.Products.FirstOrDefaultAsync(p => p.id == item.ProductId);
                }
            }
            return View(orders);
        }
        // Xử lý xóa sản phẩm
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDetail = _context.OrderDetails.Where(p => p.OrderId == id).ToList();
            foreach (var item in orderDetail)
            {
                _context.OrderDetails.Remove(item); ;
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderList");
        }
    }
}
