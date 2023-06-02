using Application.Abstraction;
using Application.Interfaces;
using Domein.AccessEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Linq.Expressions;

namespace Infrustructure.Services
{
    internal class RoleRepository : Repository<Role>,  IRoleRepository
    {
        public RoleRepository(IApplicationDbContext db) : base(db)
        {
     
        }

        public override async Task<Role?> CreateAsync(Role entity)
        {
            var role = await _Db.Roles.FirstOrDefaultAsync(x => x.ID == entity.ID);
            if (role is null)
            {
                await _Db.Roles.AddAsync(entity);
                await _Db.SaveChangesAsync();
                return _Db.Roles.First(x=>x.Name==entity.Name);
            }
            return null;
        }

        
    }
}
