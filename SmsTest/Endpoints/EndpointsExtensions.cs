namespace Sms.API.Endpoints;

public static class EndpointsExtensions
{
   public static void AddEndpoints(
        this IServiceCollection services, params Type[] scanMarkers)
   {
      var endpointDefinitions = new List<IEndpointDefinition>();

      foreach (var marker in scanMarkers)
      {
         endpointDefinitions.AddRange(
             marker.Assembly.ExportedTypes
                 .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .Select(Activator.CreateInstance).Cast<IEndpointDefinition>()
             );
      }

      foreach (var endpointDefinition in endpointDefinitions)
      {
         endpointDefinition.DefineServices(services);
      }

      services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
   }

   public static void UseEndpoints(this WebApplication app)
   {
      var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

      foreach (var endpointDefinition in definitions)
      {
         endpointDefinition.DefineEndpoints(app);
      }
   }
}
