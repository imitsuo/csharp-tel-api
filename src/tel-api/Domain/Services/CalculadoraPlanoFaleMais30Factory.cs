namespace tel_api.Domain.Services
{
    public interface ICalculadoraPlanoFaleMais30Factory
    {
        ICalculadoraCustoLigacao Create();
    }

    public class CalculadoraPlanoFaleMais30Factory: ICalculadoraPlanoFaleMais30Factory
    {
        private readonly ICalculadoraPlanoFaleMaisBuilder _calculadorFaleMaisBuilder;
        public CalculadoraPlanoFaleMais30Factory(ICalculadoraPlanoFaleMaisBuilder calculadoraFaleMaisBuilder)
        {
            _calculadorFaleMaisBuilder = calculadoraFaleMaisBuilder;
        }

        public ICalculadoraCustoLigacao Create()
        {
            return _calculadorFaleMaisBuilder.CriarPlanoFaleMais30();
        }
    }
}
