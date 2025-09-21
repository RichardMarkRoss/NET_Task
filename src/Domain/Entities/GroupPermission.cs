namespace UserGroup.Domain.Entities;


public class GroupPermission
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = default!;


    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } = default!;


    public DateTime GrantedAt { get; set; }
}