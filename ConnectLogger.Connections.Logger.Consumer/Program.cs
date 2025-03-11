using ConnectLogger.Connections.Logger.Consumer.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var builder = Host.CreateApplicationBuilder();

builder.Logging.AddConsole();
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDb(builder.Configuration);
builder.Services.AddKafka();

var host = builder.Build();

host.Run();