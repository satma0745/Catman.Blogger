namespace Catman.Blogger.WebUI.Data.Session
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Services.Auth;
    using Catman.Blogger.Core.Services.Auth.Data.Requests;

    internal class SessionStorage : ISessionStorage
    {
        public event Action OnChange;
        
        public Guid UserId { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
        
        public bool Authorized { get; private set; }
        
        public bool Loading { get; private set; }

        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public SessionStorage(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public Task Initialize() => Task.CompletedTask;

        public async Task SignIn(AuthenticateUser userInfo)
        {
            Loading = true;
            OnChange?.Invoke();
            
            var result = await _authService.Authenticate(userInfo);
            result.Consume(
                session =>
                {
                    _mapper.Map(session, this);
                    Authorized = true;
                    
                    Loading = false;
                    OnChange?.Invoke();
                },
                _ => throw new Exception("An error occured while authenticating user"));
        }

        public void SignOut()
        {
            Loading = true;
            
            UserId = Guid.Empty;
            Username = string.Empty;
            DisplayName = string.Empty;

            Authorized = false;

            Loading = false;
            OnChange?.Invoke();
        }
    }
}
