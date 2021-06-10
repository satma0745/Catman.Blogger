namespace Catman.Blogger.WebUI.Data.Session
{
    using System;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Services.Auth.Data.Requests;
    using Catman.Blogger.WebUI.Data.Common.Storage;

    internal interface ISessionStorage : IResourceStorage
    {
        Guid UserId { get; }
        
        string Username { get; }
        
        string DisplayName { get; }
        
        bool Authorized { get; }
        
        bool Loading { get; }

        Task SignIn(AuthenticateUser userInfo);

        void SignOut();
    }
}
