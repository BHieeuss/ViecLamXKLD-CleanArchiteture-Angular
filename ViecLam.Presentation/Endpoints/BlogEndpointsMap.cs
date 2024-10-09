using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using ViecLam.Presentation.Actions;

namespace ViecLam.Presentation.Endpoints
{
    public static class BlogEndpointsMap
    {
        public static IEndpointRouteBuilder MapBlogEndpoints(this IEndpointRouteBuilder app)
        {
            var blog = app.MapGroup("/api/blogs/");
            blog.MapPost("/", BlogActions.Post).WithName("AddBlog");       
            return app;
        }
    }
}
