namespace Domein.AccessEntities;

public class Role : BaseAuditableEntity
{

    public string? Name { get; set; }

    public virtual ICollection<Permission>? Permissions { get; set; }

    public virtual ICollection<AUser>? Users { get; set; }
}
