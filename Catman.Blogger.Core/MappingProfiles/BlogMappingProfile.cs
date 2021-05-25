namespace Catman.Blogger.Core.MappingProfiles
{
    using AutoMapper;
    using Catman.Blogger.Core.Persistence.Repositories.Blog.Data;
    using Catman.Blogger.Core.Services.Blog.Data.Requests;
    using Catman.Blogger.Core.Services.Blog.Data.Responses;

    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<BlogData, Blog>();
            CreateMap<CreateBlog, BlogCreationData>();
        }
    }
}
