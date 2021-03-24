using Moq;
using tel_api.Domain.Models;
using tel_api.Domain.Services;
using Xunit;

namespace test.Domain.Services
{
    public class PlanoFaleMaisBuilderTest
    {
        private string FALE_MAIS_30 = "FALE_MAIS_30";
        private string FALE_MAIS_60 = "FALE_MAIS_60";
        private string FALE_MAIS_120 = "FALE_MAIS_120";
        private PlanoFaleMaisBuilder _builder;
        private readonly Mock<IPlanoTarifaFixaFactory> _planoTarifaFixaService = new Mock<IPlanoTarifaFixaFactory>();
        private readonly Mock<IPlanoService> _planoService = new Mock<IPlanoService>();


        public PlanoFaleMaisBuilderTest()
        {
            _builder = new PlanoFaleMaisBuilder(_planoTarifaFixaService.Object);
        }

        [Fact]
        public void Test_CriarPlanoFaleMais30__PlanoFaleMais30__Expected_CriarPlanoFaleMais30()
        {
            // EXERCISE
            var result = _builder.CriarPlanoFaleMais30();

            // ASSERTS
            Assert.Equal(FALE_MAIS_30, result.Plano);
            Assert.Equal(30, ((IPlanoFaleMaisService)result).MinutosPlano);
        }

        [Fact]
        public void Test_CriarPlanoFaleMais60__PlanoFaleMais30__Expected_CriarPlanoFaleMais60()
        {
            // EXERCISE
            var result = _builder.CriarPlanoFaleMais60();

            // ASSERTS
            Assert.Equal(FALE_MAIS_60, result.Plano);
            Assert.Equal(60, ((IPlanoFaleMaisService)result).MinutosPlano);
        }

                [Fact]
        public void Test_CriarPlanoFaleMais60__PlanoFaleMais30__Expected_CriarPlanoFaleMais120()
        {
            // EXERCISE
            var result = _builder.CriarPlanoFaleMais120();

            // ASSERTS
            Assert.Equal(FALE_MAIS_120, result.Plano);
            Assert.Equal(120, ((IPlanoFaleMaisService)result).MinutosPlano);
        }
    }
}