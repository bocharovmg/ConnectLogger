using ConnectLogger.Connections.Api.Infrastructure.Configurations;
using ConnectLogger.Connections.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.OData;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDb(builder.Configuration);
builder.Services.AddODataQueryFilter();

builder.Services.AddControllers()
    .AddOData(option =>
        option.Select()
            .Filter()
            .OrderBy()
            .Expand()
            .SetMaxTop(500)
            .SkipToken());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<EnableQueryFilter>();
});

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
