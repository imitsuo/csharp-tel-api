using System.Threading.Tasks;
using Moq;
using tel_api.Domain.Models;
using tel_api.Domain.Services;
using Xunit;

namespace test.Domain.Services
{
    public class LigacaoServiceTest
    {
        private LigacaoService _service;        

        [Fact]
        public async Task Test_CalcularCusto__PlanoInformado__Expected_PlanoCalcularCusto()
        {
            // FIXTURES
            var origem = "11";
            var destino = "21";
            var tempo = 20; 
            var _plano = new Mock<IPlanoService>();
            _plano
                .Setup(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(22.2m);
            
            _service = new LigacaoService(_plano.Object, origem);

            // EXERCISE
            var result = await _service.CalcularCustoAsync(tempo, destino);

            // ASSERTS
            Assert.Equal(22.2m, result);
            _plano.Verify(x => x.CalcularValorLigacaoAsync(origem, destino, tempo));
        }
    }
}