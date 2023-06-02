
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T>? GetAllAsync(Expression<Func<T, bool>>? expression,  params string[] includes);
        Task<T?> GetByIdAsync(Guid Id);
        Task<T?> CreateAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid Id);

    }
}
