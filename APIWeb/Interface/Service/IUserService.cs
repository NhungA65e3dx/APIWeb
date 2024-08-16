using APIWeb.Entities;
using APIWeb.Model;
using System.Threading.Tasks;

namespace APIWeb.Interface.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetUserIdAsync(int id);
     
        Task CreateUserAsync(Register register);
        Task<User> LoginUser(Login login);
        Task UpdateUser(int id, Update update);
        Task DeleteUserAsync(int id);
       
    }
}
