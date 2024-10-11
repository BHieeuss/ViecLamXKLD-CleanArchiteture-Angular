using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Domain.Entities;
using ViecLam.Infrastructure.Context;

namespace ViecLam.Infrastructure.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(AppDbContext dbContext, ILogger<GenericRepository<Blog>> logger, IWebHostEnvironment env) : base(dbContext, logger, env)
        {

        }
    }
}
