using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroup.Api.Models;
using UserGroup.Infrastructure.Data;

namespace UserGroup.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController(AppDbContext db) : ControllerBase
{
    // GET /groups
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroupDto>>> GetAll()
    {
        var groups = await db.Groups
            .Select(g => new GroupDto(g.Id, g.Name, g.Description))
            .ToListAsync();
        return Ok(groups);
    }

    // GET /groups/{groupId}/users/count
    [HttpGet("{groupId:guid}/users/count")]
    public async Task<ActionResult<UsersPerGroupCountDto>> GetUsersCount(Guid groupId)
    {
        var grp = await db.Groups.FindAsync(groupId);
        if (grp is null) return NotFound();

        var count = await db.UserGroups.CountAsync(x => x.GroupId == groupId);
        return Ok(new UsersPerGroupCountDto(groupId, grp.Name, count));
    }
}
