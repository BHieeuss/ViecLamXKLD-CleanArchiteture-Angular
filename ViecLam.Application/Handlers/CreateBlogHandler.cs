using MediatR;
using Microsoft.Extensions.Logging;
using ViecLam.Application.Commands;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Application.Response;
using ViecLam.Domain.Entities;


namespace ViecLam.Application.Handlers.BlogCommandHandlers
{
    public class CreateBlogHandler : IRequestHandler<CreateBlogRequest, ServiceResponse>
    {
        private readonly IBlogRepository blogRepository;
        private readonly ILogger<CreateBlogHandler> logger;

        public CreateBlogHandler(IBlogRepository blogRepository, ILogger<CreateBlogHandler> logger)
        {
            this.blogRepository = blogRepository;
            this.logger = logger;
        }

        public async Task<ServiceResponse> Handle(CreateBlogRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = blogRepository.BeginTransaction())
            {
                try
                {
                    // Lưu ảnh và nhận tên file
                    string? imageFileName = null;
                    string productName = null; // Khởi tạo productName

                    if (request.ImageFile != null)
                    {
                        var imageResult = blogRepository.SaveImage(request.ImageFile);
                        if (imageResult.Item1 == 1)
                        {
                            imageFileName = imageResult.Item2; // Lấy tên file ảnh
                            productName = Path.GetFileNameWithoutExtension(imageFileName); // Lấy tên file không có phần mở rộng
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

                    // Kiểm tra nếu productName vẫn null, gán giá trị mặc định
                    productName ??= "DefaultProductName";

                    // Ánh xạ thủ công từ CreateBlogRequest sang Blog
                    var blog = new Blog()
                    {
                        ProductImage = imageFileName, // Lưu tên file ảnh vào trường ProductImage
                        ProductName = productName, // Lưu tên sản phẩm từ file
                        Heading = request.Heading,
                        Poster = request.Poster,
                        SubHeading = request.SubHeading,
                        BlogDetail = request.BlogDetail
                    };

                    // Thêm blog vào cơ sở dữ liệu
                    await blogRepository.AddAsync(blog);
                    await blogRepository.SaveChangeAsync();
                    await transaction.CommitAsync(cancellationToken);

                    // Tạo phản hồi thành công
                    var response = new ServiceResponse(
                        IsSuccess: true,
                        Message: "Thêm blog thành công",
                        StatusCode: 201,
                        Data: new List<object> { blog }
                    );

                    logger.LogInformation("Thêm blog thành công với ID: {BlogId}", blog.Id);
                    return response;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    logger.LogError(ex, "Đã xảy ra lỗi khi thêm blog.");
                    return new ServiceResponse(
                        IsSuccess: false,
                        Message: "Thêm blog thất bại",
                        StatusCode: 500,
                        Errors: new List<string> { ex.InnerException?.Message ?? ex.Message }
                    );
                }
            }
        }
    }
}


