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
                    // Ánh xạ thủ công từ CreateBlogRequest sang Blog
                    var blog = new Blog()
                    {
                        Image = request.Image,
                        Heading = request.Heading,
                        SubHeading = request.SubHeading,
                        BlogDate = request.BlogDate,
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
                        Errors: new List<string> { ex.Message }
                    );
                }
            }
        }
    }
}
