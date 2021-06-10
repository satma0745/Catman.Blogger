namespace Catman.Blogger.Core.MappingProfiles
{
    using AutoMapper;
    using Catman.Blogger.Core.Persistence.Repositories.User.Data;
    using Catman.Blogger.Core.Services.Auth.Data.Responses;

    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<UserData, Session>();
        }
    }
}
