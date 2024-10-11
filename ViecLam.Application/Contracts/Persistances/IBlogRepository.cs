using Microsoft.AspNetCore.Http;
using ViecLam.Domain.Entities;

namespace ViecLam.Application.Contracts.Persistances
{
    public interface IBlogRepository : IGenericReponsitory<Blog>
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);
    }
}
