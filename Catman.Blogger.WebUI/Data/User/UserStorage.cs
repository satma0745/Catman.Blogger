namespace Catman.Blogger.WebUI.Data.User
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Services.User;
    using Catman.Blogger.Core.Services.User.Data.Requests;
    using Catman.Blogger.Core.Services.User.Data.Responses;
    using Catman.Blogger.WebUI.Data.Common.Loadable;

    internal class UserStorage : IUserStorage
    {
        public event Action OnChange;

        public ILoadable<ICollection<User>> Users { get; } = new Loadable<ICollection<User>>();
        
        public bool RegisteringUser { get; private set; }
        
        public Guid UserIdToDelete { get; private set; }
        
        public bool DeletingUser { get; private set; }

        private readonly IUserService _service;

        private bool _initialized;

        public UserStorage(IUserService service)
        {
            _service = service;
        }
        
        public async Task Initialize()
        {
            if (!_initialized)
            {
                await ReloadUsers();
                _initialized = true;
            }
        }
        
        public async Task RegisterUser(RegisterUser user)
        {
            RegisteringUser = true;
            OnChange?.Invoke();
        
            var result = await _service.RegisterUser(user);
            result.Select(x => x, _ => throw new Exception("An error occured while registering user"));
        
            RegisteringUser = false;
            OnChange?.Invoke();

            await ReloadUsers();
        }

        public async Task DeleteUser(Guid userId)
        {
            UserIdToDelete = userId;
            DeletingUser = true;
            OnChange?.Invoke();
        
            var result = await _service.DeleteUser(userId);
            result.Select(x => x, _ => throw new Exception("An error occurred while deleting user"));
        
            DeletingUser = false;
            OnChange?.Invoke();

            await ReloadUsers();
        }

        private async Task ReloadUsers()
        {
            Users.Unload();
            OnChange?.Invoke();
            
            var result = await _service.GetUsers();
            var users = result.Select(x => x, _ => throw new Exception("An error occurred while loading users"));
            
            Users.Loaded(users);
            OnChange?.Invoke();
        }
    }
}
