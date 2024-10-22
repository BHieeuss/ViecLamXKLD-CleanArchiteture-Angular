using MediatR;
using ViecLam.Application.Response;

namespace ViecLam.Application.Commands.Messages
{
    public class GetMessageRequest : IRequest<ServiceResponse>
    {
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}
