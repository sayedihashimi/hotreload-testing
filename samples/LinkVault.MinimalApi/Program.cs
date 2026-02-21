using Microsoft.EntityFrameworkCore;
using LinkVault.MinimalApi.Data;
using LinkVault.MinimalApi.Endpoints;
using LinkVault.MinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=linkvault.db"));

builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IStatsService, StatsService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapLinkEndpoints();
app.MapCollectionEndpoints();
app.MapTagEndpoints();
app.MapStatsEndpoints();

app.Run();
