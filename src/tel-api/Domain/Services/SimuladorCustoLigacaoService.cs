using System.Threading.Tasks;
using FluentValidation;
using tel_api.Domain.Models;

namespace tel_api.Domain.Services
{
    public interface ISimuladorTarifaService
    {
        Task<ComparativoCustoLigacao> CompararPlanoComTarifaFixaAsync(SimulacaoCustoLigacao simulacao);
    }

    public class SimuladorTarifaService : ISimuladorTarifaService
    {
        private readonly ILigacaoBuilder _ligacaoBuilder;
        private readonly IValidator<SimulacaoCustoLigacao> _simulacaoValidator;

        public SimuladorTarifaService(ILigacaoBuilder ligacaoBuilder, IValidator<SimulacaoCustoLigacao> simulacaoValidator)
        {
            _ligacaoBuilder = ligacaoBuilder;
            _simulacaoValidator = simulacaoValidator;
        }

        public async Task<ComparativoCustoLigacao> CompararPlanoComTarifaFixaAsync(SimulacaoCustoLigacao simulacao)
        {            
            
            var result = _simulacaoValidator.Validate(simulacao);
            
            if (result.IsValid == false)
                throw new ValidationException(result.ToString(), result.Errors);

            var ligacaoPlanoFaleMais = _ligacaoBuilder
                                            .AdicionarOrigem(simulacao.DddOrigem)
                                            .AdicionarPlano(simulacao.Plano)
                                            .Criar();

            var ligacaoPlanoTarifaFixa = _ligacaoBuilder
                                            .AdicionarOrigem(simulacao.DddOrigem)
                                            .AdicionarPlano(Plano.TARIFA_FIXA)
                                            .Criar();

            var comparativo = new ComparativoCustoLigacao();
            comparativo.DddOrigem = simulacao.DddOrigem;
            comparativo.DddDestino = simulacao.DddDestino;
            comparativo.PlanoFaleMais = simulacao.Plano;
            comparativo.Tempo = simulacao.Tempo;
            comparativo.ValorComPlanoFaleMais = await ligacaoPlanoFaleMais.CalcularCustoAsync(simulacao.Tempo, simulacao.DddDestino);
            comparativo.ValorSemPlanoFaleMais = await ligacaoPlanoTarifaFixa.CalcularCustoAsync(simulacao.Tempo, simulacao.DddDestino);

            return comparativo;
        }
    }
}