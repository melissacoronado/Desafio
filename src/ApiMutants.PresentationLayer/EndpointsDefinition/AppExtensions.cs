using ApiMutants.Application.Interfaces;
using ApiMutants.Services;

namespace ApiMutants.PresentationLayer.EndpointsDefinition
{
    public static class AppExtensions
    {
        public static WebApplication MutantsEndpoints(this WebApplication app)
        {
            app.MapPost("/mutant", Endpoints.isMutant).WithName("Mutants").WithTags("Commands").AllowAnonymous();
            app.MapGet("/stats", Endpoints.Stats).WithName("Stats").WithTags("Queries").AllowAnonymous();

            return app;
        }

        public static IServiceCollection AddMutantsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMutantsService, MutantsService>();

            return services;
        }
    }
}
