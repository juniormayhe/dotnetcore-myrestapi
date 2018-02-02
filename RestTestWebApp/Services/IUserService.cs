using System.Threading.Tasks;
using RestTestWebApp.Models;

namespace RestTestWebApp.Services
{
    public interface IUserService
    {
        Task<bool> Login(UserModel userModel);
        Task<bool> Register(UserModel userModel);
    }
}