namespace Catman.Blogger.Core.Services.User.Data.Responses
{
    using System;
    using Catman.Blogger.Core.Services.Shared.Responses.Success;

    public class User : ISuccessResponse
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
    }
}
