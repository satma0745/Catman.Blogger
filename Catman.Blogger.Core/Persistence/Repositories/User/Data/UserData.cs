namespace Catman.Blogger.Core.Persistence.Repositories.User.Data
{
    using System;

    public class UserData
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
    }
}
