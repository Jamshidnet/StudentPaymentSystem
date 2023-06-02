using Application.Abstraction;
using Application.Interfaces;
using Infrustructure.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Numerics;

namespace Infrustructure.Services
{
    public  class Repository<T> :
        IRepository<T>  where T : class
    {
        protected readonly IApplicationDbContext _Db;

        public Repository(IApplicationDbContext Db)
        {
            _Db = Db;
        }

        public virtual async Task<T?> CreateAsync(T entity)
        {
            _Db.Set<T>().Add(entity);
            await _Db.SaveChangesAsync();
            return  entity;
        }

        public virtual async Task<bool> DeleteAsync(Guid Id)
        {
            var entity = _Db.Set<T>().Find(Id);
            if (entity != null)
            {
                _Db.Set<T>().Remove(entity);
                await _Db.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public virtual   IEnumerable<T>? GetAllAsync(Expression<Func<T, bool>>? expression, params string[] includes)
        {

            IQueryable<T> source = _Db.Set<T>();
            if (includes is not null)
                foreach (var item in includes)
                    source = source.Include(item);
            return source.Where(expression);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _Db.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            if (entity != null)
            {
                _Db.Set<T>().Entry(entity).State = EntityState.Modified;


                await _Db.SaveChangesAsync();
                return entity;
            }
            return null;
        }

    }
}
