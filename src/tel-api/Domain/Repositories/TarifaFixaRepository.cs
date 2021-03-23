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
            var tarifa = new TarifaFixa { };
            return null;
        } 

    }
}