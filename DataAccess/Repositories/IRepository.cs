using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IRepository<T> where T : class, BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>>? filtering,
            int? pageNumber = null,
            int pageSize = 10,
            params string[]? includes);

        Task<T?> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);
    }

}
