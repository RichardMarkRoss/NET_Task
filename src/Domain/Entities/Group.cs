namespace UserGroup.Domain.Entities;


public class Group
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }


    public byte[]? RowVersion { get; set; }


    public ICollection<UserGroupMembership> UserGroupMemberships { get; set; } = new List<UserGroupMembership>();
    public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
}