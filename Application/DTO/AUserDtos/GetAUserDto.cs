using Application.DTO.RoleDtos;
using Domein.AccessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.AUserDtos
{
    public  class GetAUserDto : AUserBaseDto
    {
        public Guid ID { get; set; }
        public IEnumerable<GetRoleDto?>? Roles { get; set; }
    }
}
