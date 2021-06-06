namespace Catman.Blogger.Core.Services.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Persistence.Repositories.Blog.Data;
    using Catman.Blogger.Core.Persistence.UnitOfWork;
    using Catman.Blogger.Core.Services.Blog.Data.Requests;
    using Catman.Blogger.Core.Services.Blog.Data.Responses;
    using Catman.Blogger.Core.Services.Shared;
    using Catman.Blogger.Core.Services.Shared.OperationResults;
    using Catman.Blogger.Core.Services.Shared.Responses.Failure;

    internal class BlogService : ServiceBase, IBlogService
    {
        private readonly IMapper _mapper;

        public BlogService(IUnitOfWorkAsyncFactory unitOfWorkFactory, IMapper mapper)
            : base(unitOfWorkFactory)
        {
            _mapper = mapper;
        }
        
        public Task<OperationResult<ICollection<Blog>>> GetBlogs() =>
            Operation(async unitOfWork =>
            {
                var blogs = await unitOfWork.Blogs.GetBlogsAsync();

                var response = _mapper.Map<ICollection<Blog>>(blogs);
                return Success(response);
            });

        public Task<OperationResult<Blog>> CreateBlog(CreateBlog request) =>
            Operation(async unitOfWork =>
            {
                if (!await unitOfWork.Blogs.TitleIsAvailableAsync(request.Title))
                {
                    var response = new ConflictFailure("This title is already taken");
                    return Failure<Blog>(response);
                }

                var creationData = _mapper.Map<BlogCreationData>(request);
                var blogId = await unitOfWork.Blogs.CreateBlogAsync(creationData);
                var blogData = await unitOfWork.Blogs.GetBlogAsync(blogId);

                var blog = _mapper.Map<Blog>(blogData);
                return Success(blog);
            });

        public Task<OperationResult> DeleteBlog(Guid blogId) =>
            Operation(async unitOfWork =>
            {
                if (!await unitOfWork.Blogs.BlogExistsAsync(blogId))
                {
                    var failure = new NotFoundFailure();
                    return Failure(failure);
                }

                await unitOfWork.Blogs.DeleteBlogAsync(blogId);
                return Success();
            });
    }
}
