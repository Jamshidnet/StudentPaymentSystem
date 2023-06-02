using Domein.AccessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAUserRepository : IRepository<AUser>
    {
         string ComputeHash(string input);
    }
}
