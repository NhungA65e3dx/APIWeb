using log4net;
using Microsoft.AspNetCore.Mvc;
using APIWeb.Interface.Service;
using APIWeb.Model;
using APIWeb.Interface.Repository;



namespace APIWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;
  
        private readonly IUserService UserService;
        private static readonly ILog _log = LogManager.GetLogger(typeof(UserController));

        public UserController(IUserService UserService, IUserRepository userRepository)
        {
            this.UserService = UserService;
            _userRepository = userRepository;
         }

        private bool ValidateUser(string Email, string password)
        {
            //checking a database
            return password == "password" && Email=="Email";
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            try
            {
                _log.Info("GetAllAsync");
                var User = await UserService.GetAllAsync();

                if (User == null || !User.Any())
                {
                    return Ok(new { message = "No User found" });
                }

                return Ok(new { message = "Successfully retrieved all User ", data = User });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all User it", error = ex.Message });
            }
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserIdAsync(int id)
        {
            try
            {
                var User = await UserService.GetUserIdAsync(id);
                if (User == null)
                {
                    return NotFound(new { message = $"User with id {id} not found" });
                }

                return Ok(new { message = "Successfully retrieved all User ", data = User });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all User it", error = ex.Message });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUserAsync(Register register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await UserService.CreateUserAsync(register);
                return Ok(new { message = "User successfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the crating User Item", error = ex.Message });
            }
        }
        [HttpPost("UserLogin")]
        public async Task<IActionResult> LoginAccount(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var response = await UserService.LoginUser(login);
                return Ok(new { message = "User Login created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the crating User Item", error = ex.Message });
            }
        }


        [HttpPut("{id:int}")]
            public async Task<IActionResult> UpdateUserAsync(int id, Update update)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var User = await UserService.GetUserIdAsync(id);
                    if (User == null)
                    {
                        return NotFound(new { message = $"User with id {id} not found" });
                    }

                    await UserService.UpdateUser(id, update);
                    return Ok(new { message = $" User Item  with id {id} successfully updated" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = $"An error occurred while updating User with id {id}", error = ex.Message });
                }
            }

            [HttpDelete("{id:int}")]
            public async Task<IActionResult> DeleteUserAsync(int id)
            {
                try
                {
                    var User = await UserService.GetUserIdAsync(id);
                    if (User == null)
                    {
                        return NotFound(new { message = $"User Item  with id {id} not found" });
                    }

                    await UserService.DeleteUserAsync(id);
                    return Ok(new { message = $"User  with id {id} successfully deleted" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = $"An error occurred while deleting User Item  with id {id}", error = ex.Message });
                }
            }
        
    }
}
