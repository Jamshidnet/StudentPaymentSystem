namespace Application.DTO.RoleDtos
{
    public  class CreateRoleDto : RoleBaseDto
    {
        public  List<Guid>? PermissionIDs { get; set; }
    }
}
