namespace Catman.Blogger.Core.Services.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Persistence.Repositories.Blog.Data;
    using Catman.Blogger.Core.Persistence.UnitOfWork;
    using Catman.Blogger.Core.Services.Blog.Data.Requests;
    using Catman.Blogger.Core.Services.Blog.Data.Responses;using Catman.Blogger.Core.Services.Shared;
    using Catman.Blogger.Core.Services.Shared.OperationResults;
    using Catman.Blogger.Core.Services.Shared.Responses.Failure;
    using Catman.Blogger.Core.Services.Shared.Responses.Success;

    internal class BlogService : ServiceBase, IBlogService
    {
        private readonly IUnitOfWorkAsyncFactory _unitOfWorkFactory;
        private readonly IMapper _mapper;

        public BlogService(IUnitOfWorkAsyncFactory unitOfWorkFactory, IMapper mapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }
        
        public async Task<OperationResult<ICollection<Blog>>> GetBlogs()
        {
            await using var unitOfWork = await _unitOfWorkFactory.CreateAsync();

            try
            {
                var blogs = await unitOfWork.Blogs.GetBlogsAsync();

                var response = _mapper.Map<ICollection<Blog>>(blogs);
                return Success(response);
            }
            catch (Exception exception)
            {
                var response = new UnexpectedFailure(exception);
                return Failure<ICollection<Blog>>(response);
            }
        }

        public async Task<OperationResult<Blog>> CreateBlog(CreateBlog request)
        {
            await using var unitOfWork = await _unitOfWorkFactory.CreateAsync();

            try
            {
                if (!await unitOfWork.Blogs.TitleIsAvailableAsync(request.Title))
                {
                    var response = new ConflictFailure("This title is already taken");
                    return Failure<Blog>(response);
                }

                var creationData = _mapper.Map<BlogCreationData>(request);
                var blogId = await unitOfWork.Blogs.CreateBlogAsync(creationData);

                var blogData = await unitOfWork.Blogs.GetBlogAsync(blogId);

                await unitOfWork.CommitAsync();

                var blog = _mapper.Map<Blog>(blogData);
                return Success(blog);
            }
            catch (Exception exception)
            {
                await unitOfWork.RollbackAsync();

                var response = new UnexpectedFailure(exception);
                return Failure<Blog>(response);
            }
        }

        public async Task<OperationResult<OperationSuccess>> DeleteBlog(Guid blogId)
        {
            await using var unitOfWork = await _unitOfWorkFactory.CreateAsync();

            try
            {
                if (!await unitOfWork.Blogs.BlogExistsAsync(blogId))
                {
                    var failure = new NotFoundFailure();
                    return Failure<OperationSuccess>(failure);
                }

                await unitOfWork.Blogs.DeleteBlogAsync(blogId);
                await unitOfWork.CommitAsync();

                return Success(new OperationSuccess());
            }
            catch (Exception exception)
            {
                var response = new UnexpectedFailure(exception);
                return Failure<OperationSuccess>(response);
            }
        }
    }
}
