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
        private readonly IMediator _mediator;

        public BlogActions(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateBlogRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteBlogRequest(id);
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateBlogRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }

    }
}
