namespace Catman.Blogger.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Persistence.Repositories.Blog;
    using Catman.Blogger.Core.Persistence.Repositories.Blog.Data;
    using Dapper;

    internal class BlogRepository : IBlogRepository
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
            var sql = "SELECT * FROM blogs";
            
            var blogs = await _connection.QueryAsync<BlogData>(sql, param: null, _transaction);
            return blogs.ToList();
        }

        public Task<BlogData> GetBlogAsync(Guid blogId)
        {
            var sql = "SELECT * FROM blogs WHERE id = @Id";
            var parameters = new {Id = blogId};

            return _connection.QuerySingleAsync<BlogData>(sql, parameters, _transaction);
        }

        public Task<bool> TitleIsAvailableAsync(string title)
        {
            var sql = "SELECT NOT EXISTS(SELECT 1 FROM blogs WHERE title = @Title)";
            var parameters = new {Title = title};

            return _connection.ExecuteScalarAsync<bool>(sql, parameters, _transaction);
        }

        public Task<bool> BlogExistsAsync(Guid blogId)
        {
            var sql = "SELECT EXISTS(SELECT 1 FROM blogs WHERE id = @Id)";
            var parameters = new {Id = blogId};

            return _connection.ExecuteScalarAsync<bool>(sql, parameters, _transaction);
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
            var sql = "DELETE FROM blogs WHERE id = @Id";
            var parameters = new {Id = blogId};

            return _connection.ExecuteAsync(sql, parameters, _transaction);
        }
    }
}
