using DA.Models;

namespace DA.Repository
{
    public interface IHotlineRepository
    {
        Task<IEnumerable<Hotline>> GetAllAsync();

        Task<Hotline> GetByIdAsync(int id);

        Task DeleteAsync(int id);
    }
}
