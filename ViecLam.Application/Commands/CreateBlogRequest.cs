using MediatR;
using System.Text.Json.Serialization;
using ViecLam.Application.Response;

namespace ViecLam.Application.Commands
{
    public class CreateBlogRequest : IRequest<ServiceResponse>
    {
        public string Image { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        [JsonIgnore]
        public DateTime BlogDate { get; set; } = DateTime.Now;
        public string BlogDetail { get; set; }
    }
}
