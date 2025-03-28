using DA.DataAccess;
using DA.Models;
using Microsoft.EntityFrameworkCore;

namespace DA.Repository
{
    public class EFHotlineRepository : IHotlineRepository
    {
        private readonly ApplicationDbContext _context;
        public EFHotlineRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Hotline>> GetAllAsync()
        {
            return await _context.Hotlines.ToListAsync();
        }
        public async Task<Hotline> GetByIdAsync(int id)
        {
            return await _context.Hotlines.FindAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var hotline = await _context.Hotlines.FindAsync(id);
            _context.Hotlines.Remove(hotline);
            await _context.SaveChangesAsync();
        }
    }
}
