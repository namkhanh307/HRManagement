using API.Models;

namespace API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByIDAsync(Guid id);

        Task<bool> Add(User entity);

        Task<bool> Update(User entity);

        Task<bool> Delete(Guid id);
    }
}
