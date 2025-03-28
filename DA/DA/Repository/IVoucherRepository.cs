using DA.Models;

namespace DA.Repository
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<Voucher>> GetAllAsync();
        Task<Voucher> GetByIdAsync(int id);
        Task AddAsync(Voucher category);
        Task UpdateAsync(Voucher category);
        Task DeleteAsync(int id);
    }
}
