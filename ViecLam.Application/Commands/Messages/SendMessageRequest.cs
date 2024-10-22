using MediatR;
using ViecLam.Application.Response;

namespace ViecLam.Application.Commands.Messages
{
    public class SendMessageRequest : IRequest<ServiceResponse>
    {
        public string SenderName { get; set; }
        public string Content { get; set; }
    }
}
