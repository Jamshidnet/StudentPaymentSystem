using Application.Abstraction;
using Application.Token;
using Domein.AccessEntities;
using Domein.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.DataAccess
{
    public class StudentPaymentDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<AUser> AUsers { get; set; }


        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }


        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }

        public StudentPaymentDbContext(DbContextOptions<StudentPaymentDbContext>? options) : base(options)
        {
        }
        private readonly DbContextOptions<StudentPaymentDbContext>? options;
        public async ValueTask<T> AddAsync<T>(T @object)
        {
            var context = new StudentPaymentDbContext(options);
            if(@object is not null)
            context.Entry(@object).State = EntityState.Added;
            await context.SaveChangesAsync();
            return @object;
        }

        public async ValueTask<T?> GetAsync<T>(params object[] objectIds) where T : class
        {
            var context = new StudentPaymentDbContext(options);
            return await context.FindAsync<T>(objectIds);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            var context = new StudentPaymentDbContext(options);
            return context.Set<T>();
        }

        public async ValueTask<T> UpdateAsync<T>(T @object)
        {
            var context = new StudentPaymentDbContext(options);
            if (@object is not null)
                context.Entry(@object).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return @object;
        }

        public async ValueTask<T> DeleteAsync<T>(T @object)
        {
            var context = new StudentPaymentDbContext(options);
            if (@object is not null)
            context.Entry(@object).State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return @object;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().Navigation(x => x.Permissions).AutoInclude();
            modelBuilder.Entity<AUser>().Navigation(x => x.Roles).AutoInclude();
            base.OnModelCreating(modelBuilder);
        }
    }
}
