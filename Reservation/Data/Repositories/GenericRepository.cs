using Domain.Interface;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        /*public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.ToListAsync();*/

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = _context.Set<T>().AsQueryable();

            var isDeletedProp = typeof(T).GetProperty("IsDeleted");
            if (isDeletedProp != null && isDeletedProp.PropertyType == typeof(bool))
            {
                query = query.Where(e =>
                    EF.Property<bool>(e, "IsDeleted") == false);
            }

            return await query.ToListAsync();
        }


        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            var isDeletedProp = typeof(T).GetProperty("IsDeleted");
            if (isDeletedProp != null && isDeletedProp.PropertyType == typeof(bool))
            {
                isDeletedProp.SetValue(entity, true);
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Soft delete requires 'IsDeleted' boolean property.");
            }
        }

    }
}
