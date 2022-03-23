using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Netboot.Logging.Extensions;
using System;
using System.IO;
using System.Reflection;

// Create web application builder.
var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
var pathContextRoot = new FileInfo(location.AbsolutePath).Directory.FullName;
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ContentRootPath = pathContextRoot,
    Args = args
});

// Add serilog implementation.
builder.Host.UseCustomSerilog();

// Add services to the container.
builder.Services.AddControllers();
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