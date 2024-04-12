using WEBANNUOCHOA.Models;
using Microsoft.EntityFrameworkCore;
// New
namespace WEBANNUOCHOA.Repositories
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
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
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
        public async Task<IEnumerable<Product>> SearchByNameAsync(string name) // Search product
        {
            if (string.IsNullOrEmpty(name))
            {
                return await GetAllAsync(); // Trả lại dữ liệu tất cả sản phẩm nếu product đó không đc tìm thấy
            }

            return await _context.Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }
    }
}

