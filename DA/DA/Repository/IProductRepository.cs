using DA.Models;

namespace DA.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetAllIdAsync(int id);

        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<IEnumerable<Product>> GetAllApple();

        Task<IEnumerable<Product>> GetAllBanPhim();
        Task<IEnumerable<Product>> GetAllChuot();

        Task<IEnumerable<Product>> GetAllLapTop();
        Task<IEnumerable<Product>> GetAllLoa();
        Task<IEnumerable<Product>> GetAllManHinh();
        Task<IEnumerable<Product>> GetAllPC();
        Task<IEnumerable<Product>> GetAllTaiNghe();
    }
}
