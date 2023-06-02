using Domein.AccessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.AUserDtos
{
    public  class CreateAUserDto :AUserBaseDto
    {
        public ICollection<Guid?>? RolesIds { get; set; }
    }
}
