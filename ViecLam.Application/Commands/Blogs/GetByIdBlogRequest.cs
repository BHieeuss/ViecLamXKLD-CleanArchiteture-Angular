using MediatR;
using ViecLam.Application.Response;

namespace ViecLam.Application.Commands.Blogs
{
    public class GetByIdBlogRequest : IRequest<ServiceResponse>
    {
        public int Id { get; set; }

        public GetByIdBlogRequest(int id)
        {
            Id = id;
        }
    }
}
