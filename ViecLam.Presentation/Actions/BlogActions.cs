using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViecLam.Application.Commands;
using Microsoft.AspNetCore.Hosting;
namespace ViecLam.Presentation.Actions
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogActions : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _environment;
        public BlogActions(IMediator mediator, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment; 
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
        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var result = await _mediator.Send(new GetAllBlogsRequest());

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var result = await _mediator.Send(new GetByIdBlogRequest(id));

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("images/{filename}")]
        public IActionResult GetImage(string filename)
        {
            var contentPath = _environment.ContentRootPath;
            var imagePath = Path.Combine(contentPath, "Uploads", filename);
            if (System.IO.File.Exists(imagePath))
            {
                var image = System.IO.File.OpenRead(imagePath);
                return File(image, "image/jpeg"); 
            }
            return NotFound();
        }

    }
}
