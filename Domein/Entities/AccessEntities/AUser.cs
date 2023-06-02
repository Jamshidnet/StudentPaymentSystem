using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domein.AccessEntities;

public class AUser :BaseAuditableEntity
{

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Role?>? Roles { get; set; }
}
