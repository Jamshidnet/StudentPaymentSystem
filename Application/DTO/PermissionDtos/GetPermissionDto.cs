using System.ComponentModel.DataAnnotations;

namespace Application.DTO.PermissionDtos;

public class GetPermissionDto
{
    public Guid ID { get; set; }
    public string? PermissionName { get; set; }
}
