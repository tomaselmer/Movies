using DataAccess.Data;
using Entities.Models;
using global::Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Service.Impl
{
   
        public class UserService : IUserService
        {
            private readonly AppDbContext _context;

            public UserService(AppDbContext context)
            {
                _context = context;
            }
            public async Task<User> GetUserById(int userId)
            {
                return await _context.Users.FindAsync(userId);
        }

            public async Task<IEnumerable<User>> GetAllUsers()
            {
            return await _context.Users.ToListAsync();
        }

            public async Task<User> CreateUser(User user)
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

            public async Task UpdateUser(User user)
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

            public async Task DeleteUser(int userId)
            {
            var movie = await _context.Movies.FindAsync(userId);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
        


