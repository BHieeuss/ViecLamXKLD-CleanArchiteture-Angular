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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateBlogRequest request)
        {
            // Gán giá trị ID từ URL và tạo một đối tượng UpdateBlogRequest từ các dữ liệu nhận được
            var updateRequest = new UpdateBlogRequest
            {
                Id = id,
                Heading = request.Heading,
                SubHeading = request.SubHeading,
                BlogDetail = request.BlogDetail,
                ImageFile = request.ImageFile,
            };

            // Gửi yêu cầu tới MediatR
            var result = await _mediator.Send(updateRequest);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }
    }
}