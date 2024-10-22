using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using ViecLam.Application.Commands.Blogs;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Application.Response;

namespace ViecLam.Application.Handlers.Blogs
{
    public class UpdateBlogHandler : IRequestHandler<UpdateBlogRequest, ServiceResponse>
    {
        private readonly IBlogRepository blogRepository;
        private readonly ILogger<UpdateBlogHandler> logger;
        private readonly IWebHostEnvironment environment;

        public UpdateBlogHandler(IBlogRepository blogRepository, ILogger<UpdateBlogHandler> logger, IWebHostEnvironment environment)
        {
            this.blogRepository = blogRepository;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task<ServiceResponse> Handle(UpdateBlogRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = blogRepository.BeginTransaction())
            {
                try
                {
                    // Tìm kiếm blog bằng `Id` từ request
                    var blog = await blogRepository.GetByIdAsync(request.Id);
                    if (blog == null)
                    {
                        return new ServiceResponse(
                            IsSuccess: false,
                            Message: "Blog không tồn tại",
                            StatusCode: 404
                        );
                    }

                    // Cập nhật các thuộc tính của blog nếu có giá trị mới
                    blog.Heading = request.Heading ?? blog.Heading;
                    blog.SubHeading = request.SubHeading ?? blog.SubHeading;
                    blog.BlogDetail = request.BlogDetail ?? blog.BlogDetail;

                    // Nếu có ảnh mới, lưu ảnh và cập nhật lại
                    if (request.ImageFile != null)
                    {
                        // Xóa ảnh cũ nếu có
                        if (!string.IsNullOrEmpty(blog.ProductImage))
                        {
                            var oldImagePath = Path.Combine(environment.ContentRootPath, "Uploads", blog.ProductImage);
                            if (File.Exists(oldImagePath))
                            {
                                File.Delete(oldImagePath);
                            }
                        }

                        // Lưu ảnh mới
                        var imageResult = blogRepository.SaveImage(request.ImageFile);
                        if (imageResult.Item1 == 1)
                        {
                            blog.ProductImage = imageResult.Item2; // Lưu tên file ảnh mới
                        }
                        else
                        {
                            return new ServiceResponse(
                                IsSuccess: false,
                                Message: imageResult.Item2,
                                StatusCode: 400
                            );
                        }
                    }

                    // Cập nhật blog vào cơ sở dữ liệu
                    await blogRepository.UpdateAsync(blog);
                    await blogRepository.SaveChangeAsync();
                    await transaction.CommitAsync(cancellationToken);

                    // Tạo phản hồi thành công
                    var response = new ServiceResponse(
                        IsSuccess: true,
                        Message: "Cập nhật blog thành công",
                        StatusCode: 200,
                        Data: new List<object> { blog }
                    );

                    logger.LogInformation("Cập nhật blog thành công với ID: {BlogId}", blog.Id);
                    return response;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    logger.LogError(ex, "Đã xảy ra lỗi khi cập nhật blog.");
                    return new ServiceResponse(
                        IsSuccess: false,
                        Message: "Cập nhật blog thất bại",
                        StatusCode: 500,
                        Errors: new List<string> { ex.InnerException?.Message ?? ex.Message }
                    );
                }
            }
        }
    }
}
