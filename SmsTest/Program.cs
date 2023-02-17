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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("api/v1/send-message", async (Domain.Entities.Sms sms, SmsRepository smsRepository) =>
{
   await smsRepository.SendSmsAsync(sms);

});

app.Run();
