namespace Catman.Blogger.Core.Persistence.Repositories.Blog.Data
{
    using System;

    public class BlogData
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
