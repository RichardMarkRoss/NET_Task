using System;

namespace UserGroup.Api.Models;

public record GroupDto(Guid Id, string Name, string? Description);
public record UsersPerGroupCountDto(Guid GroupId, string GroupName, int UserCount);
public record CountDto(long Count);
