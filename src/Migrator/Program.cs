using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserGroup.Infrastructure.Data;


var host = Host.CreateDefaultBuilder(args)
.ConfigureServices((ctx, services) =>
{
    var cs = Environment.GetEnvironmentVariable("DB_CONNECTION")
    ?? "Server=localhost,1433;Database=UserGroupDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;";
    services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(cs));
})
.Build();


using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
Console.WriteLine("Applying migrations...");
await db.Database.MigrateAsync();
Console.WriteLine("Done.");