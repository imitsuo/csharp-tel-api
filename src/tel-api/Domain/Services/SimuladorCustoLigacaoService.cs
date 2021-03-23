using System.Threading.Tasks;

namespace tel_api.Domain.Services
{
    interface ISimuladorTarifaService
    {
        Task CompararPlanoComTarifaFixaAsync(string dddOrigem, string dddDestino, int duracao);
    }

    public class SimuladorTarifaService : ISimuladorTarifaService
    {
        // private readonly ICalculadoraTarifaFixaService _calculadoraTarifaFixa;

        // public SimuladorTarifaService(ICalculadoraTarifaFixaService calculadoraTarifaFixa)
        // {
        //     _calculadoraTarifaFixa = calculadoraTarifaFixa;
        // }

        public async Task CompararPlanoComTarifaFixaAsync(string dddOrigem, string dddDestino, int duracao)
        {            
            
        }
    }
}