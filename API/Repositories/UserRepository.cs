using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(HrmanagementContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get all method error", typeof(UserRepository));
                return new List<User>();
            }
        }

        public override async Task<bool> Update(User entity)
        {
            try
            {
                var existingUser = await dbSet.Where(m => m.Id == entity.Id).FirstOrDefaultAsync();
                if (existingUser == null)
                    return await Add(entity);

                existingUser.Id = entity.Id;
                existingUser.Name = entity.Name;
                existingUser.Email = entity.Email;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update method error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(m => m.Id == id).FirstOrDefaultAsync();
                if(exist != null)
                {
                    dbSet.Remove(exist);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(UserRepository));
                return false;   
            }
        }
    }
}
