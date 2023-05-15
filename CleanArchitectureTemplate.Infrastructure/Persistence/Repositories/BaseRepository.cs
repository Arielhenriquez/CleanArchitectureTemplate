﻿using CleanArchitectureTemplate.Application.Common.Exceptions;
using CleanArchitectureTemplate.Application.Interfaces;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IBase
    {
        protected readonly IDbContext _context;
        protected readonly DbSet<TEntity> _db;
        public BaseRepository(IDbContext context)
        {
            _context = context;
            _db = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _db.AsQueryable();
        }
        public virtual async Task<TEntity> GetById(Guid id)
        {
            var entity = await Query().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (entity is null) throw new NotFoundException(typeof(TEntity).Name, id);

            return entity;
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await _db.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public virtual async Task<ICollection<TEntity>> AddRange(ICollection<TEntity> entities)
        {
            await _db.AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var dbEntity = await GetById(entity.Id);
            _context.Entry(dbEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return dbEntity;
        }

        public virtual async Task<TEntity> Delete(Guid id)
        {
            var entity = await GetById(id);

            var result = _db.Remove(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public virtual void RemoveRange(ICollection<TEntity> entities)
        {
            _db.RemoveRange(entities);
            _context.SaveChangesAsync();
        }
    }
}
