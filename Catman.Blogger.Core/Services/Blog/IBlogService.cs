namespace Catman.Blogger.Core.Services.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Services.Blog.Data.Requests;
    using Catman.Blogger.Core.Services.Blog.Data.Responses;
    using Catman.Blogger.Core.Services.Shared.OperationResults;

    public interface IBlogService
    {
        Task<OperationResult<ICollection<Blog>>> GetBlogs();

        Task<OperationResult<Blog>> CreateBlog(CreateBlog request);

        Task<OperationResult> DeleteBlog(Guid blogId);
    }
}
