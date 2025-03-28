using DA.DataAccess;
using DA.Models;
using Microsoft.EntityFrameworkCore;

namespace DA.Repository
{
    public class EFVoucherRepository : IVoucherRepository
    {

        private readonly ApplicationDbContext _context;
        public EFVoucherRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Voucher>> GetAllAsync()
        {
            return await _context.Vouchers.ToListAsync();
        }
        public async Task<Voucher> GetByIdAsync(int id)
        {
            return await _context.Vouchers.FindAsync(id);
        }
        public async Task AddAsync(Voucher voucher)
        {
            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();
        }
    }
}
