using Entities.Models;

namespace Service.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(int userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}
