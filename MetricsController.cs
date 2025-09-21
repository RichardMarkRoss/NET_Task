using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserGroup.Api.Models;
using UserGroup.Infrastructure.Data;

namespace UserGroup.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetricsController(AppDbContext db) : ControllerBase
{
    // GET /metrics/users/count
    [HttpGet("users/count")]
    public async Task<ActionResult<CountDto>> GetUserCount()
    {
        var count = await db.Users.LongCountAsync();
        return Ok(new CountDto(count));
    }
}
