using DA.Models;

namespace DA.Repository
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetAllIdAsync(int id);
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<OrderDetail>> GetAllOrdersDetailsAsync(int id);
    }
}
