using Elasticsearch.Net;
using Microsoft.OpenApi.Models;
using Nest;
using Nest.JsonNetSerializer;
using TopicSearch;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

var pool = new SingleNodeConnectionPool(new Uri(connectionStrings.ElkString));
var settings = new ConnectionSettings(pool, sourceSerializer: JsonNetSerializer.Default);
settings.BasicAuthentication(connectionStrings.username, connectionStrings.password);
var client = new ElasticClient(settings);
builder.Services.AddSingleton(client);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();