using System.ComponentModel.DataAnnotations;

namespace Application.DTO.PermissionDtos;

public class CreatePermissionDto
{
    [StringLength(50)]
    public string? PermissionName { get; set; }
}
