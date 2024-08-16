using APIWeb.Model;
namespace ClassLibrary.Contract
{
    public interface IUserAccount
    {
        Task<LoginResponse> LoginAccount(Login login);
    }
}
