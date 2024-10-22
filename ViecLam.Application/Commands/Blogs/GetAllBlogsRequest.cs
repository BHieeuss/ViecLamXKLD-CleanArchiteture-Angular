using MediatR;
using ViecLam.Application.Response;

namespace ViecLam.Application.Commands.Blogs
{
    public class GetAllBlogsRequest : IRequest<ServiceResponse>
    {
    }
}
