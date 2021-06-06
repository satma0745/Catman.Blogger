namespace Catman.Blogger.Core.Services.User
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Services.Shared.OperationResults;
    using Catman.Blogger.Core.Services.User.Data.Requests;
    using Catman.Blogger.Core.Services.User.Data.Responses;

    public interface IUserService
    {
        Task<OperationResult<ICollection<User>>> GetUsers();

        Task<OperationResult> RegisterUser(RegisterUser request);

        Task<OperationResult> DeleteUser(Guid id);
    }
}
