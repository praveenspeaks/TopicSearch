using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using TopicSearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();