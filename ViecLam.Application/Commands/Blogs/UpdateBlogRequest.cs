using MediatR;
using Microsoft.AspNetCore.Http;
using ViecLam.Application.Response;

namespace ViecLam.Application.Commands.Blogs
{
    public class UpdateBlogRequest : IRequest<ServiceResponse>
    {
        public int Id { get; set; }
        public string? Heading { get; set; }
        public string? SubHeading { get; set; }
        public string? BlogDetail { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
