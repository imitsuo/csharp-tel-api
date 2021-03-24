using System.Collections.Generic;
using FluentValidation;
using tel_api.Domain.Models;

namespace tel_api.Domain.Validators
{
    public class SimulacaoCustoLigacaoValidator : AbstractValidator<SimulacaoCustoLigacao>
    {
        public SimulacaoCustoLigacaoValidator()
        {
            RuleFor(x => x.DddDestino)
                .Must(x => Localidade.Localidades.Contains(x))
                .WithMessage($"O ddd de destino informado não é valido, ele deve ser: { string.Join(" ou ", Localidade.Localidades) }.");
            
            RuleFor(x => x.DddOrigem)
                .Must(x => Localidade.Localidades.Contains(x))
                .WithMessage($"O ddd de origem informado não é valido, ele deve ser: { string.Join(" ou ", Localidade.Localidades) }.");
            
            RuleFor(x => x.Tempo)
                .GreaterThan(0)
                .WithMessage("Tempo deve ser maior do que zero (0).");
            
            RuleFor(x => x.Plano)
                .Must(x => Plano.PlanosFaleMais.Contains(x))
                .WithMessage($"O plano informado não é valido, ele deve ser: { string.Join(" ou ", Plano.PlanosFaleMais) }.");
        }

    }
}

