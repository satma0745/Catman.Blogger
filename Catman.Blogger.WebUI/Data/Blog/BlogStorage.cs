namespace Catman.Blogger.WebUI.Data.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Services.Blog;
    using Catman.Blogger.Core.Services.Blog.Data.Requests;
    using Catman.Blogger.Core.Services.Blog.Data.Responses;
    using Catman.Blogger.WebUI.Data.Common.Loadable;

    internal class BlogStorage : IBlogStorage
    {
        public event Action OnChange;

        public ILoadable<ICollection<Blog>> Blogs { get; } = new Loadable<ICollection<Blog>>();

        public bool CreatingBlog { get; private set; }
        
        public Guid BlogIdToDelete { get; private set; }
        
        public bool DeletingBlog { get; private set; }
        
        private readonly IBlogService _service;
        
        private bool _initialized;

        public BlogStorage(IBlogService service)
        {
            _service = service;
        }

        public async Task Initialize()
        {
            if (!_initialized)
            {
                await ReloadBlogs();
                _initialized = true;
            }
        }

        public async Task CreateBlog(CreateBlog blog)
        {
            CreatingBlog = true;
            OnChange?.Invoke();

            var result = await _service.CreateBlog(blog);
            if (!result.IsSuccess)
            {
                throw new Exception("An error occurred while creating blog");
            }
            
            CreatingBlog = false;
            OnChange?.Invoke();

            await ReloadBlogs();
        }

        public async Task DeleteBlog(Guid blogId)
        {
            BlogIdToDelete = blogId;
            DeletingBlog = true;
            OnChange?.Invoke();

            var result = await _service.DeleteBlog(blogId);
            if (!result.IsSuccess)
            {
                throw new Exception($"An error occurred while deleting blog with id \"{blogId}\"");
            }
            
            DeletingBlog = false;
            OnChange?.Invoke();

            await ReloadBlogs();
        }

        private async Task ReloadBlogs()
        {
            Blogs.Unload();
            OnChange?.Invoke();
            
            var result = await _service.GetBlogs();
            var blogs = result.Select(x => x, _ => throw new Exception("An error occurred while loading blogs"));
            
            Blogs.Loaded(blogs);
            OnChange?.Invoke();
        }
    }
}
