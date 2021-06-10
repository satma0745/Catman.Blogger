namespace Catman.Blogger.Core.Services.Auth
{
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Services.Auth.Data.Requests;
    using Catman.Blogger.Core.Services.Auth.Data.Responses;
    using Catman.Blogger.Core.Services.Shared.OperationResults;

    public interface IAuthService
    {
        Task<OperationResult<Session>> Authenticate(AuthenticateUser request);
    }
}
