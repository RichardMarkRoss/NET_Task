namespace UserGroup.Domain.Entities;


public class UserGroupMembership
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;


    public Guid GroupId { get; set; }
    public Group Group { get; set; } = default!;


    public string? Role { get; set; }
    public DateTime AddedAt { get; set; }
}