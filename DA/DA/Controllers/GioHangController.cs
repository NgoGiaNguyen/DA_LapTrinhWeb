using DA.DataAccess;
using DA.Extension;
using DA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace DA.Controllers
{
    [Authorize]
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public GioHangController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult AddToCart(int productId, int quantity)
        {
            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId
            var product = GetProductFromDatabase(productId);
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
          
            var cartItem = new CartItem
            {
                Id = cart.Items.Count+1,
                ProductId = productId,
                Name = product.name,
                Image = product.imageULR,
                Price = product.price,
                Quantity = quantity
            };

          
            cart.AddItem(cartItem);
            
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }


        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart is not null)
            {
               
                    
                cart.RemoveItem(id);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index"); ;
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

        // Các actions khác...

        private Product GetProductFromDatabase(int productId)
        {
            return _context.Products.SingleOrDefault(p => p.id == productId);
        }



    }
}
