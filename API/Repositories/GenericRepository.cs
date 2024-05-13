using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected HrmanagementContext _context;

        protected DbSet<T> dbSet;

        protected readonly ILogger _logger;

        public GenericRepository(HrmanagementContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            this.dbSet = context.Set<T>();
        }
        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIDAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
