using Microsoft.EntityFrameworkCore;
using Models.Models;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly LibraryDbContext _context;

        public UserService()
        {
            _context = new LibraryDbContext();
        }


        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return false;
            }
        }

        public async Task<User> GetUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                return user;
            }
            else return null;

        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user != null)
                return user;
            else
                return null;
        }
    }
}
