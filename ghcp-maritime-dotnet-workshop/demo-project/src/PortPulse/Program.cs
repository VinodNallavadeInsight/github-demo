using Microsoft.EntityFrameworkCore;
using PortPulse.Data;
using PortPulse.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PortPulseDbContext>(options =>
{
    var conn = builder.Configuration.GetConnectionString("Default");
    if (string.IsNullOrWhiteSpace(conn)) conn = builder.Configuration["ConnectionStrings:Default"];
    if (string.IsNullOrWhiteSpace(conn)) conn = "Server=(localdb)\\mssqllocaldb;Database=PortPulse;Trusted_Connection=True;MultipleActiveResultSets=true";
    options.UseSqlServer(conn);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PortPulseDbContext>();
    await db.Database.MigrateAsync();
    await DataSeeder.SeedAsync(db, true);
}

app.MapGet("/", () => Results.Ok(new { service = "PortPulse", status = "ok" }));
app.Run();
