using ConnectLogger.Auth.Api.Infrastructure.Configurations;
using ConnectLogger.Auth.Api.Presentation.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ExceptionHandlerMiddleware>();
builder.Services.AddTransient<LogUserConnectionMiddleware>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDb(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddUseCases();
builder.Services.AddKafka();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<LogUserConnectionMiddleware>();

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
