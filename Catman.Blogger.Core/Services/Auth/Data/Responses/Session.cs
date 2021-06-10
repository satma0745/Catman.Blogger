namespace Catman.Blogger.Core.Services.Auth.Data.Responses
{
    using System;

    public class Session
    {
        public Guid UserId { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
    }
}
