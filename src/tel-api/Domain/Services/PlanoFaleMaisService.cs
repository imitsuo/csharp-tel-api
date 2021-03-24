using System.Threading.Tasks;

namespace tel_api.Domain.Services
{
    public interface IPlanoFaleMaisService
    {
        int MinutosPlano { get; }
    }

    public class PlanoFaleMaisService : IPlanoService, IPlanoFaleMaisService
    {
        private readonly string _plano;
        private readonly int _minutosPlano;
        private readonly IPlanoService _planoTarifaFixaService;

        public PlanoFaleMaisService(string plano, int minutosPlano, IPlanoTarifaFixaFactory planoTarifaFixaService)
        {
            _plano = plano;
            _minutosPlano = minutosPlano;
            _planoTarifaFixaService = planoTarifaFixaService.Instance();
        }

        public async Task<decimal> CalcularValorLigacaoAsync(string dddOrigem, string dddDestino, int tempo)
        {
            var valor = 0m;
            if (tempo > _minutosPlano)
            {
                tempo = tempo - _minutosPlano;
                valor = await _planoTarifaFixaService.CalcularValorLigacaoAsync(dddOrigem, dddDestino, tempo);
                valor = valor * 1.1m;
            }

            return valor;
        }

        public bool ehDoTipo(string plano)
        {
            return plano == _plano;
        }

        public string Plano
        {
            get { return _plano; }
        }

        public int MinutosPlano
        {
            get { return _minutosPlano; }
        }
    }


}