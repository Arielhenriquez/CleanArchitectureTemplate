using CleanArchitectureTemplate.Application.Common.PaginationResponse;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Application.Common.Extensions
{
    public static class PaginationExtension
    {
        public static async Task<Paged<T>> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken) where T : class
        {
            int page = pageNumber <= 0 ? 1 : pageNumber;
            int skip = (page - 1) * pageSize;
            return Paged<T>.Create(totalRecords: await query.CountAsync(cancellationToken), items: await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken), currentPage: page, pageSize: pageSize);
        }

        public static Paged<T> Paginate<T>(this ICollection<T> query, int pageNumber, int pageSize) where T : class
        {
            int page = pageNumber <= 0 ? 1 : pageNumber;
            int skip = (page - 1) * pageSize;
            return Paged<T>.Create(totalRecords: query.Count, items: query.Skip(skip).Take(pageSize).ToList(), currentPage: page, pageSize: pageSize);
        }
    }
}
