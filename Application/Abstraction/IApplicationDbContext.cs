using Application.Token;
using Domein.AccessEntities;
using Domein.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstraction
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>() where T : class;
        DbSet<Student> Students { get; }
        DbSet<Course> Courses { get; }
        DbSet<Payment> Payments { get; }
        DbSet<Transaction> Transactions { get; }
        DbSet<Invoice> Invoices { get; }
        DbSet<AUser> AUsers { get; }
        DbSet<Role> Roles { get; } 
        DbSet<Permission> Permissions { get; }
        DbSet<UserRefreshToken> UserRefreshToken { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

         ValueTask<T> AddAsync<T>(T @object);
         ValueTask<T?> GetAsync<T>(params object[] objectId) where T : class;
         IQueryable<T> GetAll<T>() where T : class;
         ValueTask<T> UpdateAsync<T>(T @object);
         ValueTask<T> DeleteAsync<T>(T @object);
    }
}
