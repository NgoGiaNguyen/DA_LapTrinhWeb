using DA.DataAccess;
using DA.Models;
using Microsoft.EntityFrameworkCore;

namespace DA.Repository
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Product>> GetAllIdAsync(int id)
        {
            return await _context.Products.Where(p => p.categoryId == id).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllApple()
        {
            var apple = await _context.Categories.SingleOrDefaultAsync(p => p.name.Equals("Apple"));
            return await _context.Products.Where(p => p.categoryId == apple.id).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllBanPhim()
        {
            var banPhim = await _context.Categories.SingleOrDefaultAsync(p => p.name.Equals("Bàn Phím"));
            return await _context.Products.Where(p => p.categoryId == banPhim.id).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllChuot()
        {
            var chuot = await _context.Categories.SingleOrDefaultAsync(p => p.name.Equals("Chuột"));
            return await _context.Products.Where(p => p.categoryId == chuot.id).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllLapTop()
        {
            var laptop = await _context.Categories.SingleOrDefaultAsync(p => p.name.Equals("LapTop"));
            return await _context.Products.Where(p => p.categoryId == laptop.id).ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetAllLoa()
        {
            var loa = await _context.Categories.SingleOrDefaultAsync(p => p.name.Equals("Loa"));
            return await _context.Products.Where(p => p.categoryId == loa.id).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllManHinh()
        {
            var manHinh = await _context.Categories.SingleOrDefaultAsync(p => p.name.Equals("Màn Hình"));
            return await _context.Products.Where(p => p.categoryId == manHinh.id).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllPC()
        {
            var pc = await _context.Categories.SingleOrDefaultAsync(p => p.name.Equals("PC"));
            return await _context.Products.Where(p => p.categoryId == pc.id).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllTaiNghe()
        {
            var taiNghe = await _context.Categories.SingleOrDefaultAsync(p => p.name.Equals("Tai Nghe"));
            return await _context.Products.Where(p => p.categoryId == taiNghe.id).ToListAsync();
        }
    }
}
