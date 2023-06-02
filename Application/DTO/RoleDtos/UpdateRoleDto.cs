
namespace Application.DTO.RoleDtos
{
    public  class UpdateRoleDto : RoleBaseDto
    {
        public Guid ID { get; set; }
        public ICollection<Guid>? PermissionIDs { get; set; }

    }
}
