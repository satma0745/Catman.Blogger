namespace Catman.Blogger.Core.Persistence.Repositories.User
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Persistence.Repositories.User.Data;

    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(Guid userId);

        Task<bool> UserExistsAsync(string username);

        Task<bool> UsernameIsAvailableAsync(string username);

        Task<ICollection<UserData>> GetUsersAsync();

        Task<UserData> GetUserAsync(string username);

        Task<Guid> CreateUserAsync(UserCreationData creationData);

        Task DeleteUserAsync(Guid userId);

        Task<bool> UserHasPasswordAsync(Guid userId, string password);
    }
}
