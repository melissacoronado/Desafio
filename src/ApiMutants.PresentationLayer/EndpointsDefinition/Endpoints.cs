using ApiMutants.Application.Commands;
using ApiMutants.Domain.NonEntities;
using ApiMutants.PresentationLayer.Request;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiMutants.PresentationLayer.EndpointsDefinition
{
    public class Endpoints
    {
        internal static async Task<IResult> isMutant(
            [FromServices] ILogger<Endpoints> logger,
            [FromServices] IMediator mediatr,
            [FromServices] IMapper autoMapper,
            MutantRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {

                var mutantRequest = autoMapper.Map<Mutants>(request.DNA);
                var commandService = new MutantsRqst(mutantRequest);
                bool isMutant = await mediatr.Send(commandService, cancellationToken);

                int retorno = isMutant ? StatusCodes.Status200OK : StatusCodes.Status403Forbidden;

                return isMutant
                    ? Results.Ok(retorno)
                    : Results.NotFound(retorno);
            }
            catch (Exception ex)
            {
                logger.LogError("Error isMutant", ex.Message);
                return Results.Problem(ex.Message);
            }
        }

        internal static async Task<IResult> Stats(
            [FromServices] ILogger<Endpoints> logger,
            [FromServices] IMediator mediatr,
            CancellationToken cancellationToken
        )
        {
            try
            {
                //TODO: Implementar BD y obtener consulta desde alli
                var statsResponse = new StatsResponse
                {
                    CountMutantDna = 40,
                    CountHumanDna = 100,
                    Ratio = 0.4
                };
                return Results.Ok(statsResponse);
            }
            catch (Exception ex)
            {
                logger.LogError("Error Stats", ex.Message);
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
