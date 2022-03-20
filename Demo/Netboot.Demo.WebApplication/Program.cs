using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Netboot.Logging.Extensions;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Set current directory.
// In order to create a log folder in the correct directory.
var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
var pathContextRoot = new FileInfo(location.AbsolutePath).Directory.FullName;
builder.Host.UseContentRoot(pathContextRoot);
Directory.SetCurrentDirectory(pathContextRoot);

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