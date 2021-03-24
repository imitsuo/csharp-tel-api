namespace tel_api.Domain.Services
{
    public interface IPlanoFaleMais30Factory
    {
        IPlanoService Create();
    }

    public class PlanoFaleMais30Factory: IPlanoFaleMais30Factory
    {
        private readonly IPlanoFaleMaisBuilder _faleMaisBuilder;
        public PlanoFaleMais30Factory(IPlanoFaleMaisBuilder faleMaisBuilder)
        {
            _faleMaisBuilder = faleMaisBuilder;
        }

        public IPlanoService Create()
        {
            return _faleMaisBuilder.CriarPlanoFaleMais30();
        }
    }

    public interface IPlanoFaleMais60Factory
    {
        IPlanoService Create();
    }

    public class PlanoFaleMais60Factory: IPlanoFaleMais60Factory
    {
        private readonly IPlanoFaleMaisBuilder _faleMaisBuilder;
        public PlanoFaleMais60Factory(IPlanoFaleMaisBuilder faleMaisBuilder)
        {
            _faleMaisBuilder = faleMaisBuilder;
        }

        public IPlanoService Create()
        {
            return _faleMaisBuilder.CriarPlanoFaleMais60();
        }
    }

    public interface IPlanoFaleMais120Factory
    {
        IPlanoService Create();
    }

    public class PlanoFaleMais120Factory: IPlanoFaleMais120Factory
    {
        private readonly IPlanoFaleMaisBuilder _faleMaisBuilder;
        public PlanoFaleMais120Factory(IPlanoFaleMaisBuilder faleMaisBuilder)
        {
            _faleMaisBuilder = faleMaisBuilder;
        }

        public IPlanoService Create()
        {
            return _faleMaisBuilder.CriarPlanoFaleMais120();
        }
    }
}
