namespace Catman.Blogger.Core.Services.Blog.Data.Responses
{
    using System;
    using Catman.Blogger.Core.Services.Shared.Responses.Success;

    public class Blog : ISuccessResponse
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreationDate { get; set; }
    }
}
