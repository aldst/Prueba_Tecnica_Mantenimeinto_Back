using System.Data;
using Caso3.Data;
using Caso3.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Caso3.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            const string query = "SELECT * FROM sp_get_all_users(@p_name, @p_username, @p_password, @p_email, @p_role, @p_birthdate)";
            using var connection = _context.Database.GetDbConnection();

            var parameters = new DynamicParameters();
            parameters.Add("p_username", username);
            parameters.Add("p_password", password);
            parameters.Add("p_id", null);
            parameters.Add("p_name", null);
            parameters.Add("p_email", null);
            parameters.Add("p_birthdate", null);
            parameters.Add("p_role", null);

            return await connection.QuerySingleOrDefaultAsync<User>(query, parameters, commandType: CommandType.Text);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string query = "CALL sp_delete_user(@p_id, @result)";
            using var connection = _context.Database.GetDbConnection();
            var parameters = new { p_id = id, result = 0 };
            var result = await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.Text);
            return result > 0;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync(User? user = null)
        {
            const string query = "SELECT * FROM sp_get_all_users(@p_name, @p_username, @p_password, @p_email, @p_role, @p_birthdate)";
            using var connection = _context.Database.GetDbConnection();

            var parameters = new DynamicParameters();
            parameters.Add("p_name", user?.Name);
            parameters.Add("p_email", user?.Email);
            parameters.Add("p_username", user?.Username);
            parameters.Add("p_password", user?.Password);
            parameters.Add("p_birthdate", user?.BirthDate?.Date, DbType.Date);
            parameters.Add("p_role", user?.Role);

            return await connection.QueryAsync<User>(query, parameters, commandType: CommandType.Text);
        }

        public async Task<bool> SaveAsync(User user)
        {
            const string query = "CALL sp_save_user(@p_id, @p_name, @p_username, @p_password, @p_email, @p_role, @p_birthdate)";
            using var connection = _context.Database.GetDbConnection();

            var parameters = new DynamicParameters();
            parameters.Add("p_id", user.Id);
            parameters.Add("p_name", user.Name);
            parameters.Add("p_email", user.Email);
            parameters.Add("p_username", user.Username);
            parameters.Add("p_password", user.Password);
            parameters.Add("p_birthdate", user.BirthDate?.Date, DbType.Date);
            parameters.Add("p_role", user.Role);

            await connection.ExecuteAsync(query, parameters, commandType: CommandType.Text);
            return true;
        }   

    }
}
