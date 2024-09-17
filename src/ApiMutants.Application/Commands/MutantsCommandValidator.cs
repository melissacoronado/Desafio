using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMutants.Application.Commands
{
    public class MutantsCommandValidator : AbstractValidator<MutantsRqst>
    {
        public MutantsCommandValidator()
        {
            this.RuleLevelCascadeMode = CascadeMode.Stop;
            this.ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.DNATable.DNA)
                .NotNull()
                .NotEmpty()
                .WithMessage("La tabla de DNA's no puede estar vacia.")
                .Must(dna => dna.Count >= 4)
                .WithMessage("La tabla de DNA's debe contener al menos 4 secuencias."); ;
        }
    }
}
