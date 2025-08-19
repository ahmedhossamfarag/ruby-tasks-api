using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TasksAPI.Controllers;
using TasksAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TasksAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TasksAPIContext") ?? throw new InvalidOperationException("Connection string 'TasksAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddAuthentication(UsersController.AuthenticationName)
	.AddCookie(UsersController.AuthenticationName, options =>
	{
		options.Cookie.Name = UsersController.AuthenticationName;
		options.LoginPath = "/api/users/login";
		//options.AccessDeniedPath = "/";
		options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
	});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
