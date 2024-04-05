using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using D.W.C.API.D.W.C.Service;
using D.W.C.Lib;
using D.W.C.Lib.D.W.C.Models;
using System;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<MyDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabase")));

// Configure AzureDevOpsSettings
builder.Services.Configure<AzureDevOpsSettings>(
    builder.Configuration.GetSection("AzureDevOpsSettings"));

// Register HttpClient
builder.Services.AddHttpClient();

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register AzureDevOpsClient as a service
builder.Services.AddScoped<AzureDevOpsClient>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    var settings = sp.GetRequiredService<IOptions<AzureDevOpsSettings>>();
    return new AzureDevOpsClient(httpClient, settings.Value); // U¿yj settings.Value aby uzyskaæ instancjê AzureDevOpsSettings
});

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "D.W.C. API", Version = "v1" });
    // Dodaj komentarze XML do Swagger UI, aby poprawiæ dokumentacjê API.
    var xmlFile = $"{AppDomain.CurrentDomain.FriendlyName}.xml";
    var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "D.W.C. API v1");
        c.RoutePrefix = string.Empty; 
    });
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
