using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.RoleDtos
{
    public abstract  class RoleBaseDto 
    {
        [Required]
        [StringLength(30, ErrorMessage = "the role name must be at least 30 characters. ")]
        public string? Name { get; set; }
    }
}
