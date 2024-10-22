using MediatR;
using Microsoft.Extensions.Logging;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Application.Response;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ViecLam.Application.Commands.Blogs;

namespace ViecLam.Application.Handlers.Blogs
{
    public class DeleteBlogHandler : IRequestHandler<DeleteBlogRequest, ServiceResponse>
    {
        private readonly IBlogRepository blogRepository;
        private readonly ILogger<DeleteBlogHandler> logger;
        private readonly IWebHostEnvironment environment;

        public DeleteBlogHandler(IBlogRepository blogRepository, ILogger<DeleteBlogHandler> logger, IWebHostEnvironment environment)
        {
            this.blogRepository = blogRepository;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task<ServiceResponse> Handle(DeleteBlogRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = blogRepository.BeginTransaction())
            {
                try
                {
                    var blog = await blogRepository.GetByIdAsync(request.BlogId);
                    if (blog == null)
                    {
                        return new ServiceResponse(
                            IsSuccess: false,
                            Message: "Blog không tồn tại",
                            StatusCode: 404
                        );
                    }

                    // Xóa file ảnh nếu tồn tại
                    if (!string.IsNullOrEmpty(blog.ProductImage))
                    {
                        var imagePath = Path.Combine(environment.ContentRootPath, "Uploads", blog.ProductImage);
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                            logger.LogInformation("Đã xóa ảnh: {ImagePath}", imagePath);
                        }
                        else
                        {
                            logger.LogWarning("Không tìm thấy ảnh để xóa: {ImagePath}", imagePath);
                        }
                    }

                    // Xóa blog
                    await blogRepository.DeleteAsync(request.BlogId);
                    await blogRepository.SaveChangeAsync();
                    await transaction.CommitAsync(cancellationToken);

                    return new ServiceResponse(
                        IsSuccess: true,
                        Message: "Xóa blog thành công",
                        StatusCode: 200
                    );
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    logger.LogError(ex, "Đã xảy ra lỗi khi xóa blog.");
                    return new ServiceResponse(
                        IsSuccess: false,
                        Message: "Xóa blog thất bại",
                        StatusCode: 500,
                        Errors: new List<string> { ex.InnerException?.Message ?? ex.Message }
                    );
                }
            }
        }
    }
}
