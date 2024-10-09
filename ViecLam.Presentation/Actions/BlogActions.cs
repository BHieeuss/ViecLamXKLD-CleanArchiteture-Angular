using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViecLam.Application.Commands;

namespace ViecLam.Presentation.Actions
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogActions : ControllerBase
    {   

        [HttpPost]
        public static async Task<IResult> Post([FromBody] CreateBlogRequest request, IMediator mediator)
        {
            var results = await mediator.Send(request);
            if (results.IsSuccess)
            {
                return TypedResults.Ok(results);
            }

            return TypedResults.BadRequest(results);
        }
    }
}
