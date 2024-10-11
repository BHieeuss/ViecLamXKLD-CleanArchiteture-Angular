using MediatR;
using ViecLam.Application.Response;

namespace ViecLam.Application.Commands
{
    public class DeleteBlogRequest : IRequest<ServiceResponse>
    {
        public int BlogId { get; set; }
        public DeleteBlogRequest(int blogId)
        {
            BlogId = blogId;
        }
    }
}
