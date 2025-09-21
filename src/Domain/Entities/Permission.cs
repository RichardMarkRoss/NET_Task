namespace UserGroup.Domain.Entities;


public class Permission
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string? Code { get; set; }


    public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
}