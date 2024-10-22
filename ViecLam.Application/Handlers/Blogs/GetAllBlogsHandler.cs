using MediatR;
using Microsoft.Extensions.Logging;
using ViecLam.Application.Commands.Blogs;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Application.Response;
using ViecLam.Domain.Entities;

namespace ViecLam.Application.Handlers.Blogs
{
    public class GetAllBlogsHandler : IRequestHandler<GetAllBlogsRequest, ServiceResponse>
    {
        private readonly IBlogRepository blogRepository;
        private readonly ILogger<GetAllBlogsHandler> logger;

        public GetAllBlogsHandler(IBlogRepository blogRepository, ILogger<GetAllBlogsHandler> logger)
        {
            this.blogRepository = blogRepository;
            this.logger = logger;
        }

        public async Task<ServiceResponse> Handle(GetAllBlogsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy tất cả blog từ cơ sở dữ liệu
                var blogs = await blogRepository.GetAllAsync();

                if (blogs == null || !blogs.Any())
                {
                    return new ServiceResponse(
                        IsSuccess: false,
                        Message: "Không có blog nào được tìm thấy.",
                        StatusCode: 404
                    );
                }

                // Chuyển đổi danh sách blogs sang IReadOnlyList<object>
                var blogList = blogs.Cast<object>().ToList();

                // Tạo phản hồi thành công
                var response = new ServiceResponse(
                    IsSuccess: true,
                    Message: "Lấy tất cả blog thành công",
                    StatusCode: 200,
                    Data: blogList
                );

                logger.LogInformation("Lấy tất cả blog thành công.");
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Đã xảy ra lỗi khi lấy tất cả blog.");
                return new ServiceResponse(
                    IsSuccess: false,
                    Message: "Lấy tất cả blog thất bại",
                    StatusCode: 500,
                    Errors: new List<string> { ex.InnerException?.Message ?? ex.Message }
                );
            }
        }
    }
}
