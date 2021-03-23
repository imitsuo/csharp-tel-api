namespace tel_api.Domain.Services
{
    public interface ICalculadoraPlanoFaleMaisBuilder
    {
        ICalculadoraCustoLigacao CriarPlanoFaleMais30();
        ICalculadoraCustoLigacao CriarPlanoFaleMais60();
        ICalculadoraCustoLigacao CriarPlanoFaleMais120();
    }

    public class CalculadoraPlanoFaleMaisBuilder: ICalculadoraPlanoFaleMaisBuilder
    {
        private readonly ICalculadoraCustoLigacao _calculadoraTarifaFixaService;

        public CalculadoraPlanoFaleMaisBuilder(ICalculadoraCustoLigacao calculadoraTarifaFixaService)
        {
            _calculadoraTarifaFixaService = calculadoraTarifaFixaService;
        }

        public ICalculadoraCustoLigacao CriarPlanoFaleMais30()
        {
            return new CalculadoraPlanoFaleMaisService(30, _calculadoraTarifaFixaService);
        }

        public ICalculadoraCustoLigacao CriarPlanoFaleMais60()
        {
            return new CalculadoraPlanoFaleMaisService(60, _calculadoraTarifaFixaService);
        }

        public ICalculadoraCustoLigacao CriarPlanoFaleMais120()
        {
            return new CalculadoraPlanoFaleMaisService(120, _calculadoraTarifaFixaService);
        }

    }
}