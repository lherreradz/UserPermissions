using DataAccess;
using DataAccess.Repositories;
using DataAccess.UnitOfWorks;
using DataAccess.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting.Server;
using Serilog;
using WebApi.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Elasticsearch.Net;
using Nest;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();

// Database context dependency injection
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");

var connectionString = $"Server = {dbHost}; Database = {dbName}; User Id = {dbUser}; Password = {dbPassword}";

var con = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(con));

// Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
builder.Services.AddTransient<IPermissionTypeRepository, PermissionTypeRepository>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IProducerService, ProducerService>();

// ElasticSearch

// Elasticsearch connection

builder.Services.AddTransient<ISearchRepository<Permission>, ElasticSearchRepository>();

// Add serilog

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
