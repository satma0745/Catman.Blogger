namespace Catman.Blogger.WebUI.MappingProfiles
{
    using AutoMapper;
    using Catman.Blogger.Core.Services.Auth.Data.Responses;
    using Catman.Blogger.WebUI.Data.Session;

    public class SessionMappingProfile : Profile
    {
        public SessionMappingProfile()
        {
            CreateMap<Session, SessionStorage>();
        }
    }
}
