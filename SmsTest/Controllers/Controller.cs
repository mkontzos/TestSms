using Sms.API.Endpoints;
using Sms.Infrastructure.Repository;

namespace Sms.API.Controller;

public abstract class Controller : IEndpointDefinition
{
   protected abstract string EnpointName { get; }

   public void DefineEndpoints(WebApplication app)
   {
      app.MapPost($"/{EnpointName}", Create);
   }

   internal virtual async Task<IResult> Create(
            SmsRepository smsRepository,
   Domain.Entities.Sms sms)
   {
      await smsRepository.SendSmsAsync(sms);

      return Results.Created($"/{EnpointName}/{sms.Id}", sms);
   }

   public void DefineServices(IServiceCollection services)
   {
      return;
   }
}
