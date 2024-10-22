using MediatR;
using Microsoft.Extensions.Logging;
using ViecLam.Application.Commands.Blogs;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Application.Response;

namespace ViecLam.Application.Handlers.Blogs
{
    public class GetBlogByIdHandler : IRequestHandler<GetByIdBlogRequest, ServiceResponse>
    {
        private readonly IBlogRepository blogRepository;
        private readonly ILogger<GetBlogByIdHandler> logger;

        public GetBlogByIdHandler(IBlogRepository blogRepository, ILogger<GetBlogByIdHandler> logger)
        {
            this.blogRepository = blogRepository;
            this.logger = logger;
        }

        public async Task<ServiceResponse> Handle(GetByIdBlogRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy blog theo id từ cơ sở dữ liệu
                var blog = await blogRepository.GetByIdAsync(request.Id);

                if (blog == null)
                {
                    return new ServiceResponse(
                        IsSuccess: false,
                        Message: "Blog không được tìm thấy.",
                        StatusCode: 404
                    );
                }

                // Đóng gói blog vào danh sách IReadOnlyList<object>
                var blogList = new List<object> { blog };

                // Tạo phản hồi thành công
                return new ServiceResponse(
                    IsSuccess: true,
                    Message: "Lấy blog thành công",
                    StatusCode: 200,
                    Data: blogList
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Đã xảy ra lỗi khi lấy blog.");
                return new ServiceResponse(
                    IsSuccess: false,
                    Message: "Lấy blog thất bại",
                    StatusCode: 500,
                    Errors: new List<string> { ex.InnerException?.Message ?? ex.Message }
                );
            }
        }
    }
}
