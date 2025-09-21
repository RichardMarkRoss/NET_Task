using System;

namespace UserGroup.Api.Models;

public record UserDto(Guid Id, string Username, string Email, string? FirstName, string? LastName, bool IsActive);
public record CreateUserDto(string Username, string Email, string? FirstName, string? LastName, bool IsActive = true);
public record UpdateUserDto(string? Email, string? FirstName, string? LastName, bool? IsActive);
