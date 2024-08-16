using APIWeb.Entities;
using APIWeb.Interface.Repository;
using APIWeb.Interface.Service;
using APIWeb.Model;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
namespace APIWeb.Services
{
    public class UserService : IUserService
    {
      
        private IUserRepository UserRepository;
        private IMapper mapper;
        private readonly ILogger<UserService> logger;

        public UserService(IUserRepository UserRepository, IMapper mapper, ILogger<UserService> logger)
        {
           
            this.UserRepository = UserRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var res = await UserRepository.GetAllAsync();
            if (res == null)
            {
                logger.LogInformation($" No User items found");
            }
            return res;
        }

        public async Task<User> GetUserIdAsync(int id)
        {
            var res = await UserRepository.GetByIDAsync(id);
            if (res == null)
            {
                logger.LogInformation($"No User item with Id {id} found.");
            }
            return res;
        }
        public async Task<User> LoginUser(Login login)
        {
            var res = await UserRepository.GetByEmailAsync(login.Email);
            if (res == null)
            {
                logger.LogInformation($"No User item with Id {login.Email} found.");
            }
            var pass = await UserRepository.CheckPassWord(login.Password);
            if (pass == null)
            {
                logger.LogInformation($"Pass error: {pass}");
            }
            return res;
        }
        public async Task CreateUserAsync(Register request)
        {
            try
            {
                // DATA
                var dataAdd = mapper.Map<User>(request);
                dataAdd.CreateAt = DateTime.Now;

                // CREATE & SAVE
                await UserRepository.CreateAsync(dataAdd);
                await UserRepository.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the todo item.");
            }
        }

        public async Task UpdateUser(int id, Update request)
        {
            try
            {
                // FINDED
                var dataTable = await UserRepository.GetByIDAsync(id);
                if (dataTable != null)
                {
                    var dataUpdate = mapper.Map(request, dataTable);
                    dataUpdate.UpdateAt = DateTime.Now;

                    // UPDATE & SAVE
                    UserRepository.Update(dataUpdate);
                    await UserRepository.SaveChangeAsync();
                }
                else
                {
                    logger.LogInformation($" No User items found");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating the todo item.");
            }
        }

       
        public async Task DeleteUserAsync(int id)
        {
            try
            {
                // FINDED
                var data = await UserRepository.GetByIDAsync(id);
                if (data != null)
                {
                    // DELETE & SAVE
                    UserRepository.Delete(data);
                    await UserRepository.SaveChangeAsync();
                }
                else
                {
                    logger.LogInformation($"No item found with the id {id}");
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "An error occurred while remove the todo item.");
            }
        }
    }
}
