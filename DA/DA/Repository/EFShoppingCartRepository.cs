using DA.DataAccess;
using DA.Models;
using Microsoft.EntityFrameworkCore;

namespace DA.Repository
{
    public class EFShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public EFShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrdersDetailsAsync(int id)
        {
            return await _context.OrderDetails.Where(p => p.OrderId == id).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllIdAsync(int id)
        {
            return await _context.Orders.Where(p => p.OrderDate.Month == id).ToListAsync();
        }
    }
}
