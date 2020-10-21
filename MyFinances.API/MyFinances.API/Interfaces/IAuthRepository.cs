using MyFinances.API.Models;
using System.Threading.Tasks;

namespace MyFinances.API.Interfaces
{
    interface IAuthRepository
    {
        Task<User> Login(string email, string password);
        Task<User> Regiser(User user, string password);
        Task<bool> UserExist(string email);
    }
}
