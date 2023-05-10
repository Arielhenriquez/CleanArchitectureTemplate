using CleanArchitectureTemplate.Domain.Entities;

namespace CleanArchitectureTemplate.Application.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class, IBase
    {
        IQueryable<TEntity> Query();
        Task<TEntity> GetById(Guid id);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> AddRange(ICollection<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> Delete(Guid id);
        void RemoveRange(ICollection<TEntity> entities);
    }
}
