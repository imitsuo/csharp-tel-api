namespace tel_api.Domain.Services
{
    public interface ICalculadoraPlanoFaleMais120Factory
    {
        ICalculadoraCustoLigacao Create();
    }

    public class CalculadoraPlanoFaleMais120Factory: ICalculadoraPlanoFaleMais120Factory
    {
        private readonly ICalculadoraPlanoFaleMaisBuilder _calculadorFaleMaisBuilder;
        public CalculadoraPlanoFaleMais120Factory(ICalculadoraPlanoFaleMaisBuilder calculadoraFaleMaisBuilder)
        {
            _calculadorFaleMaisBuilder = calculadoraFaleMaisBuilder;
        }

        public ICalculadoraCustoLigacao Create()
        {
            return _calculadorFaleMaisBuilder.CriarPlanoFaleMais120();
        }
    }
}