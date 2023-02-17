using Microsoft.AspNetCore.Mvc;
using Sms.API.Endpoints;
using Sms.Application.Interfaces;
using Sms.Infrastructure.Repository;

namespace Sms.API.Controller;

public class SmsController : IEndpointDefinition
{
   public void DefineEndpoints(WebApplication app)
   {
      app.MapPost($"/create", Create);
   }

   internal async Task<IResult> Create(
         [FromServices] SmsRepository smsRepository,
         [FromBody] Domain.Entities.Sms sms)
   {
      await smsRepository.SendSmsAsync(sms);

      return Results.Created($"/create/{sms.Id}", sms);
   }

   public void DefineServices(IServiceCollection services)
   {
      services.AddScoped<ISmsRepository, SmsRepository>();
      services.AddScoped<ISmsVendor, SmsVendorGRRepository>();
      services.AddScoped<ISmsVendor, SmsVendorCYRepository>();
      services.AddScoped<ISmsVendor, SmsVendorOtherRepository>();
   }
}
