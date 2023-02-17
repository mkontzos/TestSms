using Sms.API;
using Sms.API.Endpoints;
using Sms.Application.Interfaces;
using Sms.Infrastructure.Context;
using Sms.Infrastructure.Repository;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SmsDbContext>();
builder.Services.AddSingleton<ISmsRepository, SmsRepository>();
builder.Services.AddSingleton<ISmsVendor, SmsVendorGRRepository>();
builder.Services.AddSingleton<ISmsVendor, SmsVendorCYRepository>();
builder.Services.AddSingleton<ISmsVendor, SmsVendorOtherRepository>();

builder.Services.AddTransient<IDbConnection>((sp) =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpoints(typeof(IAssemblyMarker));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseEndpoints();

app.Run();
