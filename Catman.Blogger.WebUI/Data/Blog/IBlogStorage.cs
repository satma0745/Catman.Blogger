namespace Catman.Blogger.WebUI.Data.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Services.Blog.Data.Requests;
    using Catman.Blogger.Core.Services.Blog.Data.Responses;
    using Catman.Blogger.WebUI.Data.Common.Loadable;
    using Catman.Blogger.WebUI.Data.Common.Storage;

    internal interface IBlogStorage : IResourceStorage
    {
        ILoadable<ICollection<Blog>> Blogs { get; }
        
        bool CreatingBlog { get; }
        
        Guid BlogIdToDelete { get; }
        
        bool DeletingBlog { get; }

        Task CreateBlog(CreateBlog blog);
        
        Task DeleteBlog(Guid blogId);
    }
}
