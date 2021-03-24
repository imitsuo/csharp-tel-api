using System.Threading.Tasks;
using FluentValidation;
using Moq;
using tel_api.Domain.Models;
using tel_api.Domain.Repositories;
using tel_api.Domain.Services;
using Xunit;

namespace test.Domain.Services
{
    public class PlanoTarifaFixaServiceTest
    {
        private readonly PlanoTarifaFixaService _service;
        private readonly Mock<ITarifaFixaRepository> _tarifaFixaRepository = new Mock<ITarifaFixaRepository>();

        public PlanoTarifaFixaServiceTest()
        {
            _service = new PlanoTarifaFixaService(_tarifaFixaRepository.Object);
        }

        [Fact]
        public async Task Test_CalcularValorLigacaoAsync__5min__Expected_valorCobradoPorMinuto()
        {
            // FIXTURES
            var origem = "11";
            var destino = "22";
            var tempo = 10;

            _tarifaFixaRepository
                .Setup(x => x.ObterTarifaAsync(It.IsAny<string>()))
                .ReturnsAsync(new TarifaFixa 
                    {
                        Id = 1,
                        CodigoIdentificador = "11-22",
                        DddOrigem = "11",
                        DddDestino = "22",
                        Valor = 1.7m
                    });

            // EXERCISE
            var result = await _service.CalcularValorLigacaoAsync(origem, destino, tempo);

            // ASSERTS
            Assert.Equal(17m, result);
            _tarifaFixaRepository.Verify(x => x.ObterTarifaAsync("11-22"));
        }

        [Fact]
        public async Task Test_CalcularValorLigacaoAsync__LocalidadeNaoSuportada__Expected_Exception()
        {
            // FIXTURES
            var origem = "11";
            var destino = "22";
            var tempo = 10;

            _tarifaFixaRepository
                .Setup(x => x.ObterTarifaAsync(It.IsAny<string>()))
                .ReturnsAsync((TarifaFixa) null);

            // EXERCISE
            await Assert.ThrowsAsync<ValidationException>(() => _service.CalcularValorLigacaoAsync(origem, destino, tempo));

            // ASSERTS
            _tarifaFixaRepository.Verify(x => x.ObterTarifaAsync("11-22"));
        }

        [Fact]
        public void Test_ehDoTipo__PLANO_FIXA__Expected_True() 
        {
            // EXERCISE
            var result = _service.ehDoTipo("TARIFA_FIXA");
            
            // ASSERTS
            Assert.True(result);
        }

        [Fact]
        public void Test_ehDoTipo__PLANO_XXX__Expected_False() 
        {
            // EXERCISE
            var result = _service.ehDoTipo("PLANO_XXX");
            
            // ASSERTS
            Assert.False(result);
        }
    }
}