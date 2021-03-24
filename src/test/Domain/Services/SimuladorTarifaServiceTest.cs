using System;
using System.Collections.Generic;
using System.Threading;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using tel_api.Domain.Models;
using tel_api.Domain.Services;
using Xunit;

namespace test.Domain.Services
{
    public class SimuladorTarifaServiceTest
    {
        private readonly SimuladorTarifaService _service;
        private readonly Mock<IValidator<SimulacaoCustoLigacao>> _simulacaoValidator = new Mock<IValidator<SimulacaoCustoLigacao>>();
        private readonly Mock<ILigacaoBuilder> _ligacaoBuilder = new Mock<ILigacaoBuilder>();
        private readonly Mock<ILigacaoBuilder> _ligacaoTariaFixaBuilder = new Mock<ILigacaoBuilder>();
        private readonly Mock<ILigacaoBuilder> _ligacaoFaleMaisBuilder = new Mock<ILigacaoBuilder>();
        private readonly Mock<ILigacaoService> _ligacaoFaleMaisService = new Mock<ILigacaoService>();
        private readonly Mock<ILigacaoService> _ligacaoTarifaFixaService = new Mock<ILigacaoService>();        
        private string TARIFA_FIXA = "TARIFA_FIXA";
        private string FALE_MAIS_30 = "FALE_MAIS_30";

        public SimuladorTarifaServiceTest()
        {
            _service = new SimuladorTarifaService(_ligacaoBuilder.Object, _simulacaoValidator.Object);
        }            

        [Fact]
        public async void Test_CompararPlanoComTarifaFixaAsync__PlanoFaleMaisSelecionado__Expected_CriarServicePlanos_ComparativoPrecos()
        {
            // FIXTURES
            var simulacao = new SimulacaoCustoLigacao
            {
                DddOrigem = "11",
                DddDestino = "21",
                Tempo = 10,
                Plano = FALE_MAIS_30
            };

            _ligacaoFaleMaisService.Setup(x => x.CalcularCustoAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(2m);
            _ligacaoTarifaFixaService.Setup(x => x.CalcularCustoAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(22m);

            _ligacaoTariaFixaBuilder.Setup(x => x.Criar()).Returns(_ligacaoTarifaFixaService.Object);
            _ligacaoFaleMaisBuilder.Setup(x => x.Criar()).Returns(_ligacaoFaleMaisService.Object);

            _ligacaoBuilder.Setup(x => x.AdicionarOrigem(It.IsAny<string>())).Returns(_ligacaoBuilder.Object);
            _ligacaoBuilder.Setup(x => x.AdicionarPlano(TARIFA_FIXA)).Returns(_ligacaoTariaFixaBuilder.Object);
            _ligacaoBuilder.Setup(x => x.AdicionarPlano(FALE_MAIS_30)).Returns(_ligacaoFaleMaisBuilder.Object);

            _simulacaoValidator
                .Setup(x => x.Validate(It.IsAny<SimulacaoCustoLigacao>()))
                .Returns(new ValidationResult());
            
            
            // EXERCISE
            var result = await _service.CompararPlanoComTarifaFixaAsync(simulacao);

            // ASSERTS
            Assert.Equal(2m, result.ValorComPlanoFaleMais);
            Assert.Equal(22m, result.ValorSemPlanoFaleMais);
            Assert.Equal(simulacao.DddDestino, result.DddDestino);
            Assert.Equal(simulacao.DddOrigem, result.DddOrigem);
            Assert.Equal(simulacao.Tempo, result.Tempo);
            Assert.Equal(simulacao.Plano, result.PlanoFaleMais);

            _ligacaoBuilder.Verify(x => x.AdicionarOrigem(simulacao.DddOrigem), Times.Exactly(2));
            _ligacaoBuilder.Verify(x => x.AdicionarPlano(TARIFA_FIXA), Times.Exactly(1));
            _ligacaoBuilder.Verify(x => x.AdicionarPlano(FALE_MAIS_30), Times.Exactly(1));
            _ligacaoFaleMaisService.Verify(x => x.CalcularCustoAsync(simulacao.Tempo, simulacao.DddDestino));
            _ligacaoTarifaFixaService.Verify(x => x.CalcularCustoAsync(simulacao.Tempo, simulacao.DddDestino));
            _simulacaoValidator.Verify(x => x.Validate(simulacao));
        }       

        [Fact]
        public async void Test_CompararPlanoComTarifaFixaAsync__SimulacaoInvalida__Expected_Exception()
        {
            // FIXTURES
            var simulacao = new SimulacaoCustoLigacao
            {
                DddOrigem = "11",
                DddDestino = "2",
                Tempo = 10,
                Plano = "plano qualquer"
            };

            var errors = new List<ValidationFailure> 
            {
                new ValidationFailure("Plano", "O plano informado não é valido"),
                new ValidationFailure("DddDestino", "O ddd de destino informado não é valido"),
            };

            var validationResult = new ValidationResult(errors);
            _simulacaoValidator
                .Setup(x => x.Validate(It.IsAny<SimulacaoCustoLigacao>()))
                .Returns(validationResult);
            
            // EXERCISE            
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _service.CompararPlanoComTarifaFixaAsync(simulacao));

            // ASSERTS
            _ligacaoBuilder.Verify(x => x.AdicionarOrigem(It.IsAny<string>()), Times.Never);
            _ligacaoBuilder.Verify(x => x.AdicionarPlano(It.IsAny<string>()), Times.Never);
            _ligacaoFaleMaisService.Verify(x => x.CalcularCustoAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            _ligacaoTarifaFixaService.Verify(x => x.CalcularCustoAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            _simulacaoValidator.Verify(x => x.Validate(simulacao));
        }
    }
}