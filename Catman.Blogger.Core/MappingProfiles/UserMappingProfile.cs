namespace Catman.Blogger.Core.MappingProfiles
{
    using AutoMapper;
    using Catman.Blogger.Core.Persistence.Repositories.User.Data;
    using Catman.Blogger.Core.Services.User.Data.Requests;
    using Catman.Blogger.Core.Services.User.Data.Responses;

    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserData, User>();
            CreateMap<RegisterUser, UserCreationData>();
        }
    }
}
