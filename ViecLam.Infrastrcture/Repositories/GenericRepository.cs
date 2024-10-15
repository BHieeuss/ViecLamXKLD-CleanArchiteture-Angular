using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Infrastructure.Context;

namespace ViecLam.Infrastructure.Repositories
{

    public class GenericRepository<T> : IGenericReponsitory<T> where T : class
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger<GenericRepository<T>> logger;
        private IWebHostEnvironment environment;
        public GenericRepository(AppDbContext dbContext, ILogger<GenericRepository<T>> logger, IWebHostEnvironment env)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.environment = env;
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await dbContext.AddAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity to the database!");
                throw;
            }
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            try
            {
                var entity = await dbContext.Set<T>().FindAsync(id);
                if (entity != null)
                {
                    dbContext.Entry(entity).State = EntityState.Detached;
                }
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity from the database!");
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()  // Thêm phương thức này
        {
            try
            {
                return await dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entities from the database!");
                throw;
            }
        }

        public async Task DeleteAsync(object id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    dbContext.Remove(entity);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting entity from the database!");
                throw;
            }
        }
        public async Task UpdateAsync(T entity)
        {
            try
            {
                dbContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating entity in the database!");
                throw;
            }
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var contentPath = this.environment.ContentRootPath;
                // path = "c://projects/productminiapi/uploads" ,not exactly something like that
                var path = Path.Combine(contentPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extenstions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Chỉ {0} được cho phép Upload!", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                // we are trying to create a unique filename here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        }

        public async Task DeleteImage(string imageFileName)
        {
            var contentPath = this.environment.ContentRootPath;
            var path = Path.Combine(contentPath, $"Uploads", imageFileName);
            if (File.Exists(path))
                File.Delete(path);
        }

        public Task SaveChangeAsync() => dbContext.SaveChangesAsync();

        public IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }
    }
}
