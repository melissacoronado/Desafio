using ApiMutants.Application.Commands;
using ApiMutants.Domain.Config;
using ApiMutants.PresentationLayer;
using ApiMutants.PresentationLayer.EndpointsDefinition;
using ApiMutants.PresentationLayer.Mediatr;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

//Servicios
builder.Services.AddMutantsServices(builder.Configuration)
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(MutantsCommandHandler).Assembly))
    .AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddValidatorsFromAssembly(typeof(MutantsCommandValidator).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MutantsCommandHandler).Assembly)
                                        .AddOpenBehavior(typeof(RequestValidationBehavior<,>)));

builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

builder.Services.Configure<SequenceConfig>(builder.Configuration.GetSection("SequenceConfig"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiMutants V1");
    c.RoutePrefix = string.Empty;
});

//Endpoints
app.MutantsEndpoints()
    .Run();