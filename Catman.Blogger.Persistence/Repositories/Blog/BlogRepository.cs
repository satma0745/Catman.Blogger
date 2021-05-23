namespace Catman.Blogger.Persistence.Repositories.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Catman.Blogger.Persistence.Repositories.Blog.Data;
    using Dapper;

    public class BlogRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public BlogRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
        
        public async Task<ICollection<BlogData>> GetBlogsAsync()
        {
            var sql = @"SELECT * FROM blogs";
            
            var blogs = await _connection.QueryAsync<BlogData>(sql, param: null, _transaction);
            return blogs.ToList();
        }

        public Task<Guid> CreateBlogAsync(BlogCreationData creationData)
        {
            var sql = @"INSERT INTO blogs(title, description)
                        VALUES(@Title, @Description)
                        RETURNING id";
            return _connection.ExecuteScalarAsync<Guid>(sql, param: creationData, _transaction);
        }

        public Task DeleteBlogAsync(Guid blogId)
        {
            var sql = @"DELETE FROM blogs WHERE id = @Id";
            var parameters = new {Id = blogId};

            return _connection.ExecuteAsync(sql, parameters, _transaction);
        }
    }
}
