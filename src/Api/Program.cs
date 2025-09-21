using Microsoft.EntityFrameworkCore;
using UserGroup.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Conn string from appsettings or env fallback
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? Environment.GetEnvironmentVariable("DB_CONNECTION")
         ?? "Server=localhost,1433;Database=UserGroupDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(cs));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Optional: auto-migrate in dev
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapControllers();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
