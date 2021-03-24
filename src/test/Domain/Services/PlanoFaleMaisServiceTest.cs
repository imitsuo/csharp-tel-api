using System.Threading.Tasks;
using Moq;
using tel_api.Domain.Services;
using Xunit;

namespace test.Domain.Services
{
    public class PlanoFaleMaisServiceTest
    {
        private readonly PlanoFaleMaisService _service;
        private readonly string _plano = "PLANO_TESTE";
        private readonly int _minutosPlano = 11;
        private readonly Mock<IPlanoTarifaFixaFactory> _planoTarifaFixaFactory = new Mock<IPlanoTarifaFixaFactory>();
        private readonly Mock<IPlanoService> _planoTarifaFixaService = new Mock<IPlanoService>();

        public PlanoFaleMaisServiceTest()
        {
            _planoTarifaFixaFactory
                .Setup(x => x.Instance())
                .Returns(_planoTarifaFixaService.Object);

            _service = new PlanoFaleMaisService(_plano, _minutosPlano, _planoTarifaFixaFactory.Object);
        }

        [Fact]
        public async Task Test_CalcularValorLigacaoAsync__MinutosDentroDoPlano__Expected_Zero() 
        {
            // FIXTURES
            var origem = "11";
            var destino = "51";
            var tempo = 9;

            // EXERCISE
            var result = await _service.CalcularValorLigacaoAsync(origem, destino, tempo);
            
            // ASSERTS
            Assert.Equal(0, result);
            _planoTarifaFixaService.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Test_CalcularValorLigacaoAsync__20MinutosForaPlano__Expected_TarifaFixaMais10Porcento() 
        {
            // FIXTURES
            var origem = "11";
            var destino = "51";
            var tempo = 31;

            _planoTarifaFixaService
                .Setup(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(34m);

            // EXERCISE
            var result = await _service.CalcularValorLigacaoAsync(origem, destino, tempo);
            
            // ASSERTS
            Assert.Equal(37.4m, result);
            _planoTarifaFixaService.Verify(x => x.CalcularValorLigacaoAsync(origem, destino, 20));
        }

        [Fact]
        public void Test_ehDoTipo__PLANO_TESTE__Expected_True() 
        {
            // EXERCISE
            var result = _service.ehDoTipo(_plano);
            
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