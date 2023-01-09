using MediatR;
using Microsoft.EntityFrameworkCore;
using N5Challenge.Application.WebApi.Extensions;
using N5Challenge.Domain.Core;
using N5Challenge.Domain.Interfaces;
using N5Challenge.Infrastructure;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Infrastructure.Repository;
using N5Challenge.Transverse.Dto;
using Serilog;
using Serilog.Events;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Day)
);

string elasticSearchServer = builder.Configuration.GetValue<string>("ELKConfiguration:Uri");
string elasticSearchIndexName = builder.Configuration.GetValue<string>("ELKConfiguration:index");
builder.Services.AddElasticsearch(new ElasticSearchOptionsDto() { ServerUrl = elasticSearchServer, IndexName = elasticSearchIndexName });

string kafkaServer = builder.Configuration.GetValue<string>("AppSettings:kafkaServer");
string topicName = builder.Configuration.GetValue<string>("AppSettings:topicName");
builder.Services.AddSingleton<IEventManagerRepository>(new KafkaEventManagerRepository(new KafkaOptionsDto { KafkaServer = kafkaServer, TopicName = topicName }));

builder.Services.AddSingleton<IPermissionsCommandsRepository, PermissionsCommandsRepository>();
builder.Services.AddSingleton<IPermissionsQueriesRepository, PermissionsQueriesRepository>();
builder.Services.AddSingleton<IPermissionTypesCommandsRepository, PermissionTypesCommandsRepository>();
builder.Services.AddSingleton<IPermissionTypesQueriesRepository, PermissionTypesQueriesRepository>();
builder.Services.AddSingleton<IEventManagerDomain, KafkaEventManagerDomain>();
builder.Services.AddDbContext<SqlServerDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"), ServiceLifetime.Singleton);

builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), AppDomain.CurrentDomain.Load("N5Challenge.Domain"));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
