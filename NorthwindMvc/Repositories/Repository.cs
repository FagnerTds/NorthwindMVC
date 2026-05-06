
using Microsoft.EntityFrameworkCore;
using NorthwindMvc.Data;

namespace NorthwindMvc.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly NorthwindContext _context;

        public Repository(NorthwindContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAssync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAssync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAssync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
