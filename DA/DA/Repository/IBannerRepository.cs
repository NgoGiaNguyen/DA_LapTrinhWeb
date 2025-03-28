using DA.Models;

namespace DA.Repository
{
    public interface IBannerRepository
    {
        Task<IEnumerable<Banner>> GetAllAsync();
        Task<Banner> GetByIdAsync(int id);
        Task AddAsync(Banner category);
        Task UpdateAsync(Banner category);
        Task DeleteAsync(int id);
    }
}
