using MediatR;
using ViecLam.Application.Response;

namespace ViecLam.Application.Commands
{
    public class GetAllBlogsRequest : IRequest<ServiceResponse>
    {
    }
}
