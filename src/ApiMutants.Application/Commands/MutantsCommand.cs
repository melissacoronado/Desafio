using ApiMutants.Application.Interfaces;
using ApiMutants.Domain.NonEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMutants.Application.Commands
{
    public record MutantsRqst(Mutants DNATable) : IRequest<bool>;

    public class MutantsCommandHandler : IRequestHandler<MutantsRqst, bool>
    {
        private readonly ILogger<MutantsCommandHandler> _logger;
        private readonly IMutantsService _mutantService;


        public MutantsCommandHandler(ILogger<MutantsCommandHandler> logger, IMutantsService mutantService)
        {
            _logger = logger;
            _mutantService = mutantService;
        }

        public async Task<bool> Handle(MutantsRqst request, CancellationToken cancellationtoken)
        {
            try
            {
                bool retorno = _mutantService.isMutant(request.DNATable);
                return await Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
