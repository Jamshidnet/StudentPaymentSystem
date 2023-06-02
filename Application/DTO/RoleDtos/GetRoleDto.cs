using Application.DTO.PermissionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.RoleDtos
{
    public class GetRoleDto : RoleBaseDto
    {
        public Guid ID { get; set; }
        public ICollection<GetPermissionDto>? Permissions { get; set; }
    }
}
