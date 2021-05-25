namespace Catman.Blogger.Core.Persistence.Repositories.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Persistence.Repositories.Blog.Data;

    public interface IBlogRepository
    {
        Task<ICollection<BlogData>> GetBlogsAsync();

        Task<BlogData> GetBlogAsync(Guid blogId);

        Task<bool> TitleIsAvailableAsync(string title);

        Task<bool> BlogExistsAsync(Guid blogId);

        Task<Guid> CreateBlogAsync(BlogCreationData creationData);

        Task DeleteBlogAsync(Guid blogId);
    }
}
