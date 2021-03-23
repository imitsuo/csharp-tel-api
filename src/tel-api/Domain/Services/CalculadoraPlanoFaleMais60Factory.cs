namespace tel_api.Domain.Services
{
    public interface ICalculadoraPlanoFaleMais60Factory
    {
        ICalculadoraCustoLigacao Create();
    }

    public class CalculadoraPlanoFaleMais60Factory: ICalculadoraPlanoFaleMais60Factory
    {
        private readonly ICalculadoraPlanoFaleMaisBuilder _calculadorFaleMaisBuilder;
        public CalculadoraPlanoFaleMais60Factory(ICalculadoraPlanoFaleMaisBuilder calculadoraFaleMaisBuilder)
        {
            _calculadorFaleMaisBuilder = calculadoraFaleMaisBuilder;
        }

        public ICalculadoraCustoLigacao Create()
        {
            return _calculadorFaleMaisBuilder.CriarPlanoFaleMais60();
        }
    }
}