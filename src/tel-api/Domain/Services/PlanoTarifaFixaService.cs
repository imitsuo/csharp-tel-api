using System;
using System.Threading.Tasks;
using FluentValidation;
using tel_api.Domain.Models;
using tel_api.Domain.Repositories;

namespace tel_api.Domain.Services
{
    public interface IPlanoTarifaFixaFactory
    {
        IPlanoService Instance();
    }

    public class PlanoTarifaFixaService : IPlanoService, IPlanoTarifaFixaFactory
    {
        private readonly string _plano = Models.Plano.TARIFA_FIXA;
        private readonly ITarifaFixaRepository _tarifaFixaRepository;

        public PlanoTarifaFixaService(ITarifaFixaRepository tarifaFixaRepository)
        {
            _tarifaFixaRepository = tarifaFixaRepository;
        }

        public async Task<decimal> CalcularValorLigacaoAsync(string dddOrigem, string dddDestino, int duracao)
        {
            var identificador = $"{dddOrigem}-{dddDestino}";
            var tarifa = await _tarifaFixaRepository.ObterTarifaAsync(identificador);

            if (tarifa != null)
                return  duracao * tarifa.Valor;
            
            throw new ValidationException("DDD não suportado");
        }

        public bool ehDoTipo(string plano)
        {
            return _plano == plano;
        }

        public IPlanoService Instance()
        {
            return this;
        }

        public string Plano
        {
            get { return _plano; }
        }
    }
}