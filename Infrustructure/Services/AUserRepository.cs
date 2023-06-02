using Application.Abstraction;
using Application.Interfaces;
using Domein.AccessEntities;
using System.Security.Cryptography;
using System.Text;

namespace Infrustructure.Services
{
    public class AUserRepository : Repository<AUser> , IAUserRepository
    {
        public AUserRepository(IApplicationDbContext _db) : base(_db)
        {
       
        }
        public string ComputeHash(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder builder = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            } 

            return builder.ToString();
        }

        public override async Task<AUser?> CreateAsync(AUser entity)
        {
                if (_Db.AUsers.FirstOrDefault(x => x.Username == entity.Username) is not null || entity.Roles is null)
                return null;
            if(entity.Password is not null)
                entity.Password = ComputeHash(entity.Password);
                await _Db.AUsers.AddAsync(entity);
                await _Db.SaveChangesAsync();
            return entity;
        }

            }
}
