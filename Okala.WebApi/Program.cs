using Okala.Application.DTOs.Configuration;
using Okala.Application.Extensions;
using Okala.Infrastructure.Extensions;
using Okala.WebApi.Extensions;
using Okala.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigs(builder);
builder.Services.AddWebApi(builder);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();