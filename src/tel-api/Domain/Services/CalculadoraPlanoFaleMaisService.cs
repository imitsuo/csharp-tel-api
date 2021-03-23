using System.Threading.Tasks;

namespace tel_api.Domain.Services
{
    public class CalculadoraPlanoFaleMaisService : ICalculadoraCustoLigacao
    {
        private readonly int _minutosPlano;
        private readonly ICalculadoraCustoLigacao _calculadoraTarifaFixaService;

        public CalculadoraPlanoFaleMaisService(int minutosPlano, ICalculadoraCustoLigacao calculadoraTarifaFixaService)
        {
            _minutosPlano = minutosPlano;
            _calculadoraTarifaFixaService = calculadoraTarifaFixaService;
        }

        public async Task<decimal> CalcularValorLigacaoAsync(string dddOrigem, string dddDestino, int duracao)
        {
            var valor = 0m;
            if (duracao > _minutosPlano)
            {
                duracao = duracao - _minutosPlano;
                valor = await _calculadoraTarifaFixaService.CalcularValorLigacaoAsync(dddOrigem, dddDestino, duracao);
            }

            return valor;
        }
    }

}