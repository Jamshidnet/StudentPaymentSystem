using Application.Abstraction;
using Application.Interfaces;
using Domein.AccessEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrustructure.Services
{
    public class PermissionRepository : Repository<Permission> , IPermissionRepository
    {
        public PermissionRepository(IApplicationDbContext Db) : base(Db)
        {
        }
    }
}
