using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services.Interfaces
{
   public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<bool> CreateUserAsync(User user);
        Task<User> LoginAsync(string username , string password);


    }
}
