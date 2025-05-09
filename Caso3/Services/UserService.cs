using Caso3.Models;
using Caso3.Repositories;

namespace Caso3.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync(User? user = null) => await _userRepository.GetAllUserAsync(user);

        public async Task<bool> SaveAsync(User user) => await _userRepository.SaveAsync(user);

        public async Task<bool> DeleteAsync(int id) => await _userRepository.DeleteAsync(id);

        public async Task<User?> AuthenticateAsync(string username, string password) => await _userRepository.AuthenticateAsync(username, password);
    }
}
