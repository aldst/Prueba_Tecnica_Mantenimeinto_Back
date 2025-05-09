using Caso3.Models;

namespace Caso3.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUserAsync(User? user);
        Task<bool> SaveAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
