using LibraryAPI.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions
    (options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//builder.Services.AddDbContext<LibraryDbContext>(opts =>
//{
//    var azureString = builder.Configuration.GetConnectionString("AzureDb");
//    var connBuilder = new SqlConnectionStringBuilder(azureString)
//    {
//        Password = builder.Configuration["DbPassword"]
//    };
//    azureString = connBuilder.ConnectionString;
//    opts.UseSqlServer(azureString);
//});

builder.Services.AddDbContext<LibraryDbContext>(opt => opt.UseSqlServer
    (builder.Configuration.GetConnectionString("LibraryDb")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
