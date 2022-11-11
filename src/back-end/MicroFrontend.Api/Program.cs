using MicroFrontend.Api.Common.Interfaces;
using MicroFrontend.Api.Common.Models;
using MicroFrontend.Api.Persistence;
using MicroFrontend.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

// Setting and providing db context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("MainDatabase");
    options.UseSqlServer(connectionString);
});
builder.Services.AddTransient<IAppDbContext>(provider => provider.GetService<AppDbContext>() ?? throw new InvalidOperationException());

// Other tools
builder.Services.AddAutoMapper(typeof(Program));

// Services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<AppDbContextInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await initializer.InitializeAsync();
}

app.UseHttpsRedirection();

// users endpoints
string usersUrl = "/users";
app.MapGet(usersUrl, async (IUserService repo) => await repo.GetAsync());
app.MapGet(usersUrl + "/{id}", 
    async (IUserService repo, string id) => await repo.GetByIdAsync(id));
app.MapPost(usersUrl,
    async (IUserService repo, UserDto requestBody) => await repo.UpsertAsync(requestBody))
    .WithMetadata(new SwaggerOperationAttribute(summary: "Upsert (covers both insert and update)", description: "Send null as the id to do insertion, if you fill the id field it will try to update (edit)."));
app.MapDelete(usersUrl + "/{id}", async (IUserService repo, string id) => await repo.DeleteAsync(id));

app.Run();
