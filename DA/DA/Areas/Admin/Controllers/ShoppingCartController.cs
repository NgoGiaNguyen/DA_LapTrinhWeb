using DA.DataAccess;
using DA.Models;
using DA.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ApplicationDbContext _context;
        public ShoppingCartController(ApplicationDbContext context, IProductRepository productRepository,
        ICategoryRepository categoryRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<IActionResult> Index(int id)
        {
            var shoppingCart = await _shoppingCartRepository.GetAllAsync();
            ThongKe thongKe = new ThongKe();
           
            if (id != 0)
            {
                shoppingCart = await _shoppingCartRepository.GetAllIdAsync(id);
            }

            foreach (var item in shoppingCart)
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


    }
}
