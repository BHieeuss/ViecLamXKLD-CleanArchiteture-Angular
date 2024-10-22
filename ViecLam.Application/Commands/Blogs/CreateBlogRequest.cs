using MediatR;
using Microsoft.AspNetCore.Http;
using ViecLam.Application.Response;

namespace ViecLam.Application.Commands.Blogs
{
    public class CreateBlogRequest : IRequest<ServiceResponse>
    {
        public IFormFile ImageFile { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public string Poster { get; set; }
        public string BlogDetail { get; set; }
    }
}
