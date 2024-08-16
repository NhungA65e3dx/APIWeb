using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace APIWeb.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext appDbContext;
        private DbSet<T> entities;

        public Repository(DbContext context)
        {
            this.appDbContext = context;
            entities = appDbContext.Set<T>();
        }
       
        public Task SaveChangeAsync() => appDbContext.SaveChangesAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T?> GetByIDAsync(int id)
        {
            var entity = await entities.FindAsync(id);
            if (entity == null)
            {
                return null!;
            }
            return entity;
        }
        public async Task<T?> CheckPassWord(String Password)
        {
            var entity = await entities.FindAsync(Password);
            if (entity == null)
            {
                return null!;
            }
            return entity;
        }
        public async Task<T?> GetByEmailAsync(String Email)
        {
            var entity = await entities.FindAsync(Email);
            if (entity == null)
            {
                return null!;
            }
            return entity;
        }
        public async Task CreateAsync(T entity)
        {
            await entities.AddAsync(entity);
        }

        public async Task<T?> LoginAsync(String Email, string Password)
        {
            var entity = await entities.FindAsync(Email, Password);
            if (entity == null||Email==null||Password==null)
            {
                return null!;
            }
         
            return entity;
        }
        public void Delete(T entity)
        {
            entities.Remove(entity);
        }

        public void Update(T entity)
        {
            entities.Update(entity);
        }
    }
}
