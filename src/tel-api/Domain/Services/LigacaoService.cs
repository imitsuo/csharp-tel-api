using System.Collections.Generic;
using System.Threading.Tasks;

namespace tel_api.Domain.Services
{
    public interface ILigacaoBuilder
    {
        ILigacaoBuilder AdicionarPlano(string plano);
        ILigacaoBuilder AdicionarOrigem(string dddOrigem);
        ILigacaoService Criar();
    }

    public class LigacaoBuilder: ILigacaoBuilder
    {
        private List<IPlanoService> _planos;
        private IPlanoService _plano;
        private string _dddOrigem;

        public LigacaoBuilder(IPlanoTarifaFixaFactory planoTarifaFixaService, 
                              IPlanoFaleMais30Factory planoFaleMais30Factory, 
                              IPlanoFaleMais60Factory planoFaleMais60Factory, 
                              IPlanoFaleMais120Factory planoFaleMais120Factory)
        {
            _planos = new List<IPlanoService>() 
            { 
                planoTarifaFixaService.Instance(), planoFaleMais30Factory.Create(), planoFaleMais60Factory.Create(), planoFaleMais120Factory.Create() 
            };
        }

        public ILigacaoBuilder AdicionarOrigem(string dddOrigem)
        {
            _dddOrigem = dddOrigem;
            return this;
        }

        public ILigacaoBuilder AdicionarPlano(string plano)
        {
            foreach(var _servico in _planos)
            {
                if (_servico.ehDoTipo(plano))
                {
                    _plano = _servico;
                    break;
                }
            }

            return this;
        }

        public ILigacaoService Criar()
        {
            return new LigacaoService(_plano, _dddOrigem);
        }
    }

    public interface ILigacaoService
    {
        Task<decimal> CalcularCustoAsync(int tempo, string dddDestino);
        string DddOrigem { get; }
        string Plano { get; }
    }

    public class LigacaoService: ILigacaoService
    {
        IPlanoService _planoService;
        private string _dddOrigem;

        public LigacaoService(IPlanoService planoService, string dddOrigem)
        {
            _planoService = planoService;
            _dddOrigem = dddOrigem;
        }

        public async Task<decimal> CalcularCustoAsync(int tempo, string dddDestino)
        {
            return await _planoService.CalcularValorLigacaoAsync(_dddOrigem, dddDestino, tempo);
        }

        public string DddOrigem 
        {
            get { return _dddOrigem; }
        }

        public string Plano
        {
            get { return _planoService.Plano; }
        }
    }
}