using System.Collections.Generic;
using System.Threading.Tasks;
using tel_api.Domain.Models;

namespace tel_api.Domain.Services
{
    public interface ICalculadoraCustoLigacao
    {
        Task<decimal> CalcularValorLigacaoAsync(string dddOrigem, string dddDestino, int duracao);        
    }

    public interface ICalculadoraCustoLigacaoService
    {
        Task<decimal> CalcularValorLigacaoAsync(EnumPlano plano, string dddOrigem, string dddDestino, int duracao);
    }

    public class CalculadoraCustoLigacaoService : ICalculadoraCustoLigacaoService
    {
        private Dictionary<EnumPlano, ICalculadoraCustoLigacao> _dicCalculadoras;
        private readonly ICalculadoraCustoLigacao _calculadoraTarifaFixaService;
        private readonly ICalculadoraCustoLigacao _calculadoraPlanoFaleMais30Service;
        private readonly ICalculadoraCustoLigacao _calculadoraPlanoFaleMais60Service;
        private readonly ICalculadoraCustoLigacao _calculadoraPlanoFaleMais120Service;

        public CalculadoraCustoLigacaoService(
                                                ICalculadoraTarifaFixaFactory calculadoraTarifaFixa,
                                                ICalculadoraPlanoFaleMais30Factory calculadoraPlanoFaleMais30,
                                                ICalculadoraPlanoFaleMais60Factory calculadoraPlanoFaleMais60,
                                                ICalculadoraPlanoFaleMais120Factory calculadoraPlanoFaleMais120
        )
        {
            _calculadoraTarifaFixaService =  calculadoraTarifaFixa.Create();
            _calculadoraPlanoFaleMais30Service = calculadoraPlanoFaleMais30.Create();
            _calculadoraPlanoFaleMais60Service = calculadoraPlanoFaleMais60.Create();
            _calculadoraPlanoFaleMais120Service = calculadoraPlanoFaleMais120.Create();

            _dicCalculadoras = new Dictionary<EnumPlano, ICalculadoraCustoLigacao>()
            {
                { EnumPlano.TarifaFixa, _calculadoraTarifaFixaService },
                { EnumPlano.FaleMais30, _calculadoraPlanoFaleMais30Service },
                { EnumPlano.FaleMais60, _calculadoraPlanoFaleMais60Service },
                { EnumPlano.FaleMais120, _calculadoraPlanoFaleMais120Service },
            };
        }

        public async Task<decimal> CalcularValorLigacaoAsync(EnumPlano plano, string dddOrigem, string dddDestino, int duracao)
        {
            if (_dicCalculadoras.TryGetValue(plano, out var _calculadora) == false)
                throw new System.NotImplementedException();

            var result = await _calculadora.CalcularValorLigacaoAsync(dddOrigem, dddDestino, duracao);
            
            return result;
        }
    }
}