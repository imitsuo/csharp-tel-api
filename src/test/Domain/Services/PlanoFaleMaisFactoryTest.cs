using Moq;
using tel_api.Domain.Models;
using tel_api.Domain.Services;
using Xunit;

namespace test.Domain.Services
{
    public class PlanoFaleMais30FactoryTest
    {
        private PlanoFaleMais30Factory _factory;
        private readonly Mock<IPlanoFaleMaisBuilder> _planoFaleMaisBuilder = new Mock<IPlanoFaleMaisBuilder>();
        private readonly Mock<IPlanoService> _planoService = new Mock<IPlanoService>();

        public PlanoFaleMais30FactoryTest()
        {
            _factory = new PlanoFaleMais30Factory(_planoFaleMaisBuilder.Object);
        }

        [Fact]
        public void Test_Create__CriarPlanoFaleMais30__Expected_PlanoFaleMais30Service()
        {
            // FIXTURES
            var planoService = new Mock<IPlanoService>();
            _planoFaleMaisBuilder
                .Setup(x => x.CriarPlanoFaleMais30())
                .Returns(planoService.Object);

            // EXERCISE
            var result = _factory.Create();

            // ASSERTS
            Assert.True(planoService.Object.Equals(result));
            _planoFaleMaisBuilder.Verify(x => x.CriarPlanoFaleMais30());
        }
    }

    public class PlanoFaleMais60FactoryTest
    {
        private PlanoFaleMais60Factory _factory;
        private readonly Mock<IPlanoFaleMaisBuilder> _planoFaleMaisBuilder = new Mock<IPlanoFaleMaisBuilder>();
        private readonly Mock<IPlanoService> _planoService = new Mock<IPlanoService>();

        public PlanoFaleMais60FactoryTest()
        {
            _factory = new PlanoFaleMais60Factory(_planoFaleMaisBuilder.Object);
        }

        [Fact]
        public void Test_Create__CriarPlanoFaleMais60__Expected_PlanoFaleMais60Service()
        {
            // FIXTURES
            var planoService = new Mock<IPlanoService>();
            _planoFaleMaisBuilder
                .Setup(x => x.CriarPlanoFaleMais60())
                .Returns(planoService.Object);

            // EXERCISE
            var result = _factory.Create();

            // ASSERTS
            Assert.True(planoService.Object.Equals(result));
            _planoFaleMaisBuilder.Verify(x => x.CriarPlanoFaleMais60());
        }
    }

    public class PlanoFaleMais120FactoryTest
    {
        private PlanoFaleMais120Factory _factory;
        private readonly Mock<IPlanoFaleMaisBuilder> _planoFaleMaisBuilder = new Mock<IPlanoFaleMaisBuilder>();
        private readonly Mock<IPlanoService> _planoService = new Mock<IPlanoService>();

        public PlanoFaleMais120FactoryTest()
        {
            _factory = new PlanoFaleMais120Factory(_planoFaleMaisBuilder.Object);
        }

        [Fact]
        public void Test_Create__CriarPlanoFaleMais120__Expected_PlanoFaleMais120Service()
        {
            // FIXTURES
            var planoService = new Mock<IPlanoService>();
            _planoFaleMaisBuilder
                .Setup(x => x.CriarPlanoFaleMais120())
                .Returns(planoService.Object);

            // EXERCISE
            var result = _factory.Create();

            // ASSERTS
            Assert.True(planoService.Object.Equals(result));
            _planoFaleMaisBuilder.Verify(x => x.CriarPlanoFaleMais120());
        }
    }
}