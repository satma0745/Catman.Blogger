namespace Catman.Blogger.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Persistence.Repositories.User;
    using Catman.Blogger.Core.Persistence.Repositories.User.Data;
    using Dapper;

    internal class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public UserRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<ICollection<UserData>> GetUsersAsync()
        {
            var sql = "SELECT * FROM users";
            
            var users = await _connection.QueryAsync<UserData>(sql, param: null, _transaction);
            return users.ToList();
        }
        
        public Task<UserData> GetUserAsync(string username)
        {
            var sql = "SELECT * FROM users WHERE username = @Username";
            var parameters = new {Username = username};

            return _connection.QuerySingleAsync<UserData>(sql, parameters, _transaction);
        }

        public Task<bool> UserExistsAsync(Guid userId)
        {
            var sql = "SELECT EXISTS(SELECT 1 FROM users WHERE id = @Id)";
            var parameters = new {Id = userId};

            return _connection.ExecuteScalarAsync<bool>(sql, parameters, _transaction);
        }
        
        public Task<bool> UserExistsAsync(string username)
        {
            var sql = "SELECT EXISTS(SELECT 1 FROM users WHERE username = @Username)";
            var parameters = new {Username = username};

            return _connection.ExecuteScalarAsync<bool>(sql, parameters, _transaction);
        }

        public Task<bool> UsernameIsAvailableAsync(string username)
        {
            var sql = "SELECT NOT EXISTS(SELECT 1 FROM users WHERE username = @Username)";
            var parameters = new {Username = username};

            return _connection.ExecuteScalarAsync<bool>(sql, parameters, _transaction);
        }

        public Task<Guid> CreateUserAsync(UserCreationData creationData)
        {
            var sql = @"INSERT INTO users(username, password, display_name)
                        VALUES(@Username, @Password, @DisplayName)
                        RETURNING id";
            return _connection.ExecuteScalarAsync<Guid>(sql, param: creationData, _transaction);
        }

        public Task DeleteUserAsync(Guid userId)
        {
            var sql = "DELETE FROM users WHERE id = @Id";
            var parameters = new {Id = userId};

            return _connection.ExecuteAsync(sql, parameters, _transaction);
        }

        public Task<bool> UserHasPasswordAsync(Guid userId, string password)
        {
            var sql = "SELECT EXISTS(SELECT 1 FROM users WHERE id = @Id AND password = @Password)";
            var parameters = new {Id = userId, Password = password};

            return _connection.ExecuteScalarAsync<bool>(sql, parameters, _transaction);
        }
    }
}
