using MyFinances.API.Models;
using System;
using System.Threading.Tasks;

namespace MyFinances.API.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Login(string email, string password);
        Task<User> Register(User user, string password);
        Task<bool> UserExist(string email);
        Task<User> GetUser(Guid userId);
    }
}
