using System;
using System.Threading.Tasks;
using tel_api.Domain.Repositories;

namespace tel_api.Domain.Services
{
    public interface ICalculadoraTarifaFixaFactory
    {
        ICalculadoraCustoLigacao Create();
    }

    public class CalculadoraTarifaFixaFactory: ICalculadoraTarifaFixaFactory
    {
        private readonly ITarifaFixaRepository _tarifaFixaRepository;

        public CalculadoraTarifaFixaFactory(ITarifaFixaRepository tarifaFixaRepository)
        {
            _tarifaFixaRepository = tarifaFixaRepository;
        }

        public ICalculadoraCustoLigacao Create()
        {
            return new CalculadoraTarifaFixaService(_tarifaFixaRepository);
        }
    }

    public class CalculadoraTarifaFixaService : ICalculadoraCustoLigacao
    {
        private readonly ITarifaFixaRepository _tarifaFixaRepository;

        public CalculadoraTarifaFixaService(ITarifaFixaRepository tarifaFixaRepository)
        {
            _tarifaFixaRepository = tarifaFixaRepository;
        }

        public async Task<decimal> CalcularValorLigacaoAsync(string dddOrigem, string dddDestino, int duracao)
        {
            var identificador = $"{dddOrigem}-{dddDestino}";
            var tarifa = await _tarifaFixaRepository.ObterTarifaAsync(identificador);

            if (tarifa != null)
                return  duracao * tarifa.Valor;
            
            // TODO: Customizar Exception
            throw new Exception("DDD não suportado");
        }
    }
}