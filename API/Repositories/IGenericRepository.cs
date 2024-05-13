namespace API.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIDAsync(Guid id);

        Task<bool> Add(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(Guid id);
    }
}
