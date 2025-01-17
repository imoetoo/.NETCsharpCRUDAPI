//program.cs

//utilise the Todo model and DB context (for injection) in the program.cs file
using TodoAPI.AppDataContext;
using TodoAPI.Models;
using TodoAPI.Interface;
using TodoAPI.Services;
using TodoAPI.Middleware;
using TodoAPI.Contracts;
using AutoMapper; // Add this using directive




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Inject/Register the DbSettings into the service container in .NET Core
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddSingleton<TodoDbContext>();

// Add our Global Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

//Adding of the TodoService to the service container
builder.Services.AddLogging();

//Register our Service and Iservices in the service container
builder.Services.AddScoped<ITodoServices, TodoServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(); //Use the global exception handler

app.UseAuthorization();

app.MapControllers();

app.Run();