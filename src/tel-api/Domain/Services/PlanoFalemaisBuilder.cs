using tel_api.Domain.Models;

namespace tel_api.Domain.Services
{
    public interface IPlanoFaleMaisBuilder
    {
        IPlanoService CriarPlanoFaleMais30();
        IPlanoService CriarPlanoFaleMais60();
        IPlanoService CriarPlanoFaleMais120();
    }

    public class PlanoFaleMaisBuilder: IPlanoFaleMaisBuilder
    {
        private readonly IPlanoTarifaFixaFactory _planoTarifaFixaService;

        public PlanoFaleMaisBuilder(IPlanoTarifaFixaFactory planoTarifaFixaService)
        {
            _planoTarifaFixaService = planoTarifaFixaService;
        }

        public IPlanoService CriarPlanoFaleMais30()
        {
            return new PlanoFaleMaisService(Plano.FALE_MAIS_30, 30, _planoTarifaFixaService);
        }

        public IPlanoService CriarPlanoFaleMais60()
        {
            return new PlanoFaleMaisService(Plano.FALE_MAIS_60, 60, _planoTarifaFixaService);
        }

        public IPlanoService CriarPlanoFaleMais120()
        {
            return new PlanoFaleMaisService(Plano.FALE_MAIS_120, 120, _planoTarifaFixaService);
        }

    }
}