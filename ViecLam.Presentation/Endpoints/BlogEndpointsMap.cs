using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ViecLam.Application.Commands;
using ViecLam.Presentation.Actions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ViecLam.Presentation.Endpoints
{
    public static class BlogEndpointsMap
    {
        public static IEndpointRouteBuilder MapBlogEndpoints(this IEndpointRouteBuilder app)
        {
            var blog = app.MapGroup("/api/blogs/");

            // Tạo blog
            blog.MapPost("/", async (HttpContext httpContext, [FromServices] IMediator mediator) =>
            {
                var form = httpContext.Request.Form;
                var createBlogRequest = new CreateBlogRequest
                {
                    ImageFile = form.Files["ImageFile"],
                    Heading = form["Heading"],
                    SubHeading = form["SubHeading"],
                    BlogDetail = form["BlogDetail"]
                };

                var result = await mediator.Send(createBlogRequest);

                if (result.IsSuccess)
                {
                    return Results.Ok(result);
                }

                return Results.BadRequest(result);
            }).WithName("AddBlog");

            // Xóa blog
            blog.MapDelete("/{id}", async (int id, [FromServices] IMediator mediator) =>
            {
                var request = new DeleteBlogRequest(id);
                var result = await mediator.Send(request);

                if (result.IsSuccess)
                {
                    return Results.Ok(result);
                }

                return Results.Json(result, statusCode: result.StatusCode);
            }).WithName("DeleteBlog");

            // Cập nhật Blog
            blog.MapPut("/", async (int id, HttpContext httpContext, [FromServices] IMediator mediator) =>
            {
                var form = httpContext.Request.Form;
                var updateBlogRequest = new CreateBlogRequest
                {
                    ImageFile = form.Files["ImageFile"],
                    Heading = form["Heading"],
                    SubHeading = form["SubHeading"],
                    BlogDetail = form["BlogDetail"]
                };

                var result = await mediator.Send(updateBlogRequest);

                return result.IsSuccess ? Results.Ok(result) : Results.Json(result, statusCode: result.StatusCode);
            }).WithName("UpdateBlog");
            return app;
        }
    }
}
