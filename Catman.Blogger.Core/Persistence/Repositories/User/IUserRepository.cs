namespace Catman.Blogger.Core.Persistence.Repositories.User
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Persistence.Repositories.User.Data;

    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(Guid userId);

        Task<bool> UsernameIsAvailableAsync(string username);

        Task<ICollection<UserData>> GetUsersAsync();

        Task<Guid> CreateUserAsync(UserCreationData creationData);

        Task DeleteUserAsync(Guid userId);
    }
}
