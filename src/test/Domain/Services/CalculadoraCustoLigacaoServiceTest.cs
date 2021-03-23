using Moq;
using tel_api.Domain.Models;
using tel_api.Domain.Services;
using Xunit;

namespace test.Domain.Services
{
    public class CalculadoraCustoLigacaoTest
    {
        private readonly CalculadoraCustoLigacaoService _service;
        private readonly Mock<ICalculadoraTarifaFixaFactory> _calculadoraTarifaFixaFactory = new Mock<ICalculadoraTarifaFixaFactory>();
        private readonly Mock<ICalculadoraPlanoFaleMais30Factory> _calculadoraPlanoFaleMais30Factory = new Mock<ICalculadoraPlanoFaleMais30Factory>();
        private readonly Mock<ICalculadoraPlanoFaleMais60Factory> _calculadoraPlanoFaleMais60Factory = new Mock<ICalculadoraPlanoFaleMais60Factory>();
        private readonly Mock<ICalculadoraPlanoFaleMais120Factory> _calculadoraPlanoFaleMais120Factory = new Mock<ICalculadoraPlanoFaleMais120Factory>();
        private readonly Mock<ICalculadoraCustoLigacao> _calculadoraPlanoFaleMais30Service = new Mock<ICalculadoraCustoLigacao>();
        private readonly Mock<ICalculadoraCustoLigacao> _calculadoraPlanoFaleMais60Service = new Mock<ICalculadoraCustoLigacao>();
        private readonly Mock<ICalculadoraCustoLigacao> _calculadoraPlanoFaleMais120Service = new Mock<ICalculadoraCustoLigacao>();
        private readonly Mock<ICalculadoraCustoLigacao> _calculadoraTarifaFixaService = new Mock<ICalculadoraCustoLigacao>();


        public CalculadoraCustoLigacaoTest()
        {
            _calculadoraTarifaFixaFactory.Setup(x => x.Create()).Returns(_calculadoraTarifaFixaService.Object);
            _calculadoraPlanoFaleMais30Factory.Setup(x => x.Create()).Returns(_calculadoraPlanoFaleMais30Service.Object);
            _calculadoraPlanoFaleMais60Factory.Setup(x => x.Create()).Returns(_calculadoraPlanoFaleMais60Service.Object);
            _calculadoraPlanoFaleMais120Factory.Setup(x => x.Create()).Returns(_calculadoraPlanoFaleMais120Service.Object);

            _service = new CalculadoraCustoLigacaoService(
                                                          _calculadoraTarifaFixaFactory.Object, 
                                                          _calculadoraPlanoFaleMais30Factory.Object, 
                                                          _calculadoraPlanoFaleMais60Factory.Object, 
                                                          _calculadoraPlanoFaleMais120Factory.Object);

        }

        [Fact]
        public async void Test_CalcularValorLigacaoAsync__LigacaoComTarifaFixa__Expected_CriarTarifaFixaServiceECalcularValorLigacao()
        {
            // FIXTURES
            var dddOrigem = "011";
            var dddDestino = "021";
            var duracao = 10;

            _calculadoraTarifaFixaService.Setup(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(11.1m);
            
            // EXERCISE
            var result = await _service.CalcularValorLigacaoAsync(EnumPlano.TarifaFixa, dddOrigem, dddDestino, duracao);

            // ASSERTS
            Assert.Equal(11.1m, result);
            _calculadoraTarifaFixaService.Verify(x => x.CalcularValorLigacaoAsync(dddOrigem, dddDestino, duracao));
            _calculadoraPlanoFaleMais30Service.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _calculadoraPlanoFaleMais60Service.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _calculadoraPlanoFaleMais120Service.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async void Test_CalcularValorLigacaoAsync__LigacaoPlanoFaleMais30__Expected_CriarPlanoFaleMais30ServiceECalcularValorLigacao()
        {
            // FIXTURES
            var dddOrigem = "011";
            var dddDestino = "021";
            var duracao = 12;

            _calculadoraPlanoFaleMais30Service.Setup(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(1.1m);
            
            // EXERCISE
            var result = await _service.CalcularValorLigacaoAsync(EnumPlano.FaleMais30, dddOrigem, dddDestino, duracao);

            // ASSERTS
            Assert.Equal(1.1m, result);            
            _calculadoraPlanoFaleMais30Service.Verify(x => x.CalcularValorLigacaoAsync(dddOrigem, dddDestino, duracao));
            _calculadoraTarifaFixaService.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _calculadoraPlanoFaleMais60Service.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _calculadoraPlanoFaleMais120Service.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async void Test_CalcularValorLigacaoAsync__LigacaoPlanoFaleMais60__Expected_CriarPlanoFaleMais60ServiceECalcularValorLigacao()
        {
            // FIXTURES
            var dddOrigem = "011";
            var dddDestino = "021";
            var duracao = 13;

            _calculadoraPlanoFaleMais60Service.Setup(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(2.2m);
            
            // EXERCISE
            var result = await _service.CalcularValorLigacaoAsync(EnumPlano.FaleMais60, dddOrigem, dddDestino, duracao);

            // ASSERTS
            Assert.Equal(2.2m, result);            
            _calculadoraPlanoFaleMais60Service.Verify(x => x.CalcularValorLigacaoAsync(dddOrigem, dddDestino, duracao));
            _calculadoraTarifaFixaService.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _calculadoraPlanoFaleMais30Service.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _calculadoraPlanoFaleMais120Service.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async void Test_CalcularValorLigacaoAsync__LigacaoPlanoFaleMais120__Expected_CriarPlanoFaleMais120ServiceECalcularValorLigacao()
        {
            // FIXTURES
            var dddOrigem = "011";
            var dddDestino = "021";
            var duracao = 14;

            _calculadoraPlanoFaleMais120Service.Setup(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(3.3m);
            
            // EXERCISE
            var result = await _service.CalcularValorLigacaoAsync(EnumPlano.FaleMais120, dddOrigem, dddDestino, duracao);

            // ASSERTS
            Assert.Equal(3.3m, result);            
            _calculadoraPlanoFaleMais120Service.Verify(x => x.CalcularValorLigacaoAsync(dddOrigem, dddDestino, duracao));
            _calculadoraTarifaFixaService.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _calculadoraPlanoFaleMais30Service.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            _calculadoraPlanoFaleMais60Service.Verify(x => x.CalcularValorLigacaoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        }
    }
}