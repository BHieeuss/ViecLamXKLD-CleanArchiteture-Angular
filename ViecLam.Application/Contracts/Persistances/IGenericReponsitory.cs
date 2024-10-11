﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace ViecLam.Application.Contracts.Persistances
{
    public interface IGenericReponsitory <T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetByIdAsync(object id);
        Task DeleteAsync(object id);
        Task UpdateAsync(T entity);
        Task SaveChangeAsync();
        Tuple<int, string> SaveImage(IFormFile imageFile);
        Task DeleteImage(string imageFileName);
        IDbContextTransaction BeginTransaction();
    }
}
