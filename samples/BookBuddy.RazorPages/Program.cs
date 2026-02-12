using BookBuddy.RazorPages.Data;
using BookBuddy.RazorPages.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add DbContext with SQLite
builder.Services.AddDbContext<BookBuddyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BookBuddyDatabase") 
        ?? "Data Source=bookbuddy.db"));

// Add application services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReadingStatsService, ReadingStatsService>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IReadingSessionService, ReadingSessionService>();

var app = builder.Build();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookBuddyContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
