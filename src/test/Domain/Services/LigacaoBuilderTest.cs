using Moq;
using tel_api.Domain.Models;
using tel_api.Domain.Services;
using Xunit;

namespace test.Domain.Services
{
    public class LigacaoBuilderTest
    {
        private string TARIFA_FIXA = "TARIFA_FIXA";
        private string FALE_MAIS_30 = "FALE_MAIS_30";
        private string FALE_MAIS_60 = "FALE_MAIS_60";
        private string FALE_MAIS_120 = "FALE_MAIS_120";
        private readonly LigacaoBuilder _builder;
        private readonly Mock<IPlanoTarifaFixaFactory> _tarifaFixaFactory = new Mock<IPlanoTarifaFixaFactory>();
        private readonly Mock<IPlanoFaleMais30Factory> _planoFaleMais30Factory = new Mock<IPlanoFaleMais30Factory>();
        private readonly Mock<IPlanoFaleMais60Factory> _planoFaleMais60Factory = new Mock<IPlanoFaleMais60Factory>();
        private readonly Mock<IPlanoFaleMais120Factory> _planoFaleMais120Factory = new Mock<IPlanoFaleMais120Factory>();
        private readonly Mock<IPlanoService> _planoTarifaFixaService = new Mock<IPlanoService>();
        private readonly Mock<IPlanoService> _planoFaleMais30Service = new Mock<IPlanoService>();
        private readonly Mock<IPlanoService> _planoFaleMais60Service = new Mock<IPlanoService>();
        private readonly Mock<IPlanoService> _planoFaleMais120Service = new Mock<IPlanoService>();

        public LigacaoBuilderTest()
        {
            _tarifaFixaFactory
                .Setup(x => x.Instance())
                .Returns(_planoTarifaFixaService.Object);
            
            _planoFaleMais30Factory
                .Setup(x => x.Create())
                .Returns(_planoFaleMais30Service.Object);
            
            _planoFaleMais60Factory
                .Setup(x => x.Create())
                .Returns(_planoFaleMais60Service.Object);
            
            _planoFaleMais120Factory
                .Setup(x => x.Create())
                .Returns(_planoFaleMais120Service.Object);

            _builder = new LigacaoBuilder(
                _tarifaFixaFactory.Object, _planoFaleMais30Factory.Object, _planoFaleMais60Factory.Object, _planoFaleMais120Factory.Object
            );
        }

        [Fact]
        public void Test_Criar__PlanoTarifaFixa__ExpectedLigacaoComPlanoTarifaFixa()
        {
            // FIXTURES
            var origem = "11";
            _planoTarifaFixaService.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(true);
            _planoTarifaFixaService.Setup(x => x.Plano).Returns(TARIFA_FIXA);
            _planoFaleMais30Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);
            _planoFaleMais60Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);
            _planoFaleMais120Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);

            // EXERCISE
            var result = _builder
                            .AdicionarOrigem(origem)
                            .AdicionarPlano(TARIFA_FIXA)
                            .Criar();

            // ASSERTS
            Assert.Equal(origem, result.DddOrigem);
            Assert.Equal(TARIFA_FIXA, result.Plano);
            _planoTarifaFixaService.Verify(x => x.ehDoTipo(TARIFA_FIXA));
        }

        [Fact]
        public void Test_Criar__PlanoFaleMais30__ExpectedLigacaoComPlanoFaleMais30()
        {
            // FIXTURES
            var origem = "11";
            _planoTarifaFixaService.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);            
            _planoFaleMais30Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(true);
            _planoFaleMais30Service.Setup(x => x.Plano).Returns(FALE_MAIS_30);
            _planoFaleMais60Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);
            _planoFaleMais120Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);

            // EXERCISE
            var result = _builder
                            .AdicionarOrigem(origem)
                            .AdicionarPlano(FALE_MAIS_30)
                            .Criar();

            // ASSERTS
            Assert.Equal(origem, result.DddOrigem);
            Assert.Equal(FALE_MAIS_30, result.Plano);
            _planoFaleMais30Service.Verify(x => x.ehDoTipo(FALE_MAIS_30));
        }

        [Fact]
        public void Test_Criar__PlanoFaleMais60__ExpectedLigacaoComPlanoFaleMais60()
        {
            // FIXTURES
            var origem = "11";
            _planoTarifaFixaService.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);            
            _planoFaleMais30Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);            
            _planoFaleMais60Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(true);
            _planoFaleMais60Service.Setup(x => x.Plano).Returns(FALE_MAIS_60);
            _planoFaleMais120Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);

            // EXERCISE
            var result = _builder
                            .AdicionarOrigem(origem)
                            .AdicionarPlano(FALE_MAIS_60)
                            .Criar();

            // ASSERTS
            Assert.Equal(origem, result.DddOrigem);
            Assert.Equal(FALE_MAIS_60, result.Plano);
            _planoFaleMais60Service.Verify(x => x.ehDoTipo(FALE_MAIS_60));
        }

        [Fact]
        public void Test_Criar__PlanoFaleMais120__ExpectedLigacaoComPlanoFaleMais120()
        {
            // FIXTURES
            var origem = "11";
            _planoTarifaFixaService.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);            
            _planoFaleMais30Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);            
            _planoFaleMais60Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(false);            
            _planoFaleMais120Service.Setup(x => x.ehDoTipo(It.IsAny<string>())).Returns(true);
            _planoFaleMais120Service.Setup(x => x.Plano).Returns(FALE_MAIS_120);

            // EXERCISE
            var result = _builder
                            .AdicionarOrigem(origem)
                            .AdicionarPlano(FALE_MAIS_120)
                            .Criar();

            // ASSERTS
            Assert.Equal(origem, result.DddOrigem);
            Assert.Equal(FALE_MAIS_120, result.Plano);
            _planoFaleMais120Service.Verify(x => x.ehDoTipo(FALE_MAIS_120));
        }
    }
}