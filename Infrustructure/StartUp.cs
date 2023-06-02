using Application.Abstraction;
using Application.Interfaces;
using Infrustructure.DataAccess;
using Infrustructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

namespace Infrustructure
{
    public static class StartUp
    {
      
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StudentPaymentDbContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"), b => b.MigrationsAssembly("StudentPaymentSystem"));
                });
            services.AddScoped<IApplicationDbContext, StudentPaymentDbContext>();
            return services;
        }
        public static string ComputeSha256Hash(this string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes); 
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
          //  Console.WriteLine("DI injected instance. ");
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAUserRepository, AUserRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserServiceRepository, UserServiceRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            
            return services;
        }


    }
}
