using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domein.AccessEntities;

public class Permission : BaseAuditableEntity
{
  
    public string? PermissionName { get; set; }

    public virtual ICollection<Role>? Roles { get; set; }

}
