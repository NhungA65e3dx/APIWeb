namespace APIWeb.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIDAsync(int id);
        Task<T?> GetByEmailAsync(String Email);
        Task<T?> CheckPassWord(String Password);
        Task CreateAsync(T entity);
        Task <T?> LoginAsync(String Email, String Password);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangeAsync();

    }
}
