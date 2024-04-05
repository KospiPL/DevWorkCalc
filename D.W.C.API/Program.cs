using D.W.C.API.D.W.C.Service;
using D.W.C.Lib;
using D.W.C.Lib.D.W.C.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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
builder.Services.AddAutoMapper(typeof(WorkItemProfile));

// Register AzureDevOpsClient as a service
builder.Services.AddScoped<AzureDevOpsClient>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    var settings = sp.GetRequiredService<IOptions<AzureDevOpsSettings>>();
    return new AzureDevOpsClient(httpClient, settings);
});

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "D.W.C. API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();