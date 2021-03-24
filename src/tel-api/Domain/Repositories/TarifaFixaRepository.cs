using System.Collections.Generic;
using System.Threading.Tasks;
using tel_api.Domain.Models;

namespace tel_api.Domain.Repositories
{
    public interface ITarifaFixaRepository
    {
        Task<TarifaFixa> ObterTarifaAsync(string identificador);
    }

    public class TarifaFixaRepository: ITarifaFixaRepository
    {
        public TarifaFixaRepository()
        {

        }

        public async Task<TarifaFixa> ObterTarifaAsync(string identificador)
        {
            var tarifas = new Dictionary<string, TarifaFixa>()
            {
                {"011-016", new TarifaFixa { Id=1, CodigoIdentificador="011-016", DddOrigem="011", DddDestino="016", Valor=1.4m } },
                {"011-017", new TarifaFixa { Id=1, CodigoIdentificador="011-017", DddOrigem="011", DddDestino="017", Valor=1.7m } },
                {"018-011", new TarifaFixa { Id=1, CodigoIdentificador="018-011", DddOrigem="018", DddDestino="011", Valor=1.9m } },
            };

            if (tarifas.TryGetValue(identificador, out var _tarifa))
                return _tarifa;
            
            return null;
        } 

    }
}