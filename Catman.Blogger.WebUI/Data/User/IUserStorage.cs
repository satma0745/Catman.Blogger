namespace Catman.Blogger.WebUI.Data.User
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Services.User.Data.Requests;
    using Catman.Blogger.Core.Services.User.Data.Responses;
    using Catman.Blogger.WebUI.Data.Common.Loadable;
    using Catman.Blogger.WebUI.Data.Common.Storage;

    internal interface IUserStorage : IResourceStorage
    {
        ILoadable<ICollection<User>> Users { get; }

        bool RegisteringUser { get; }
        
        Guid UserIdToDelete { get; }
        
        bool DeletingUser { get; }

        Task RegisterUser(RegisterUser user);
        
        Task DeleteUser(Guid userId);
    }
}
