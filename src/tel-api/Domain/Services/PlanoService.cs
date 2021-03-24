using System.Threading.Tasks;

namespace tel_api.Domain.Services
{
    public interface IPlanoService
    {
        Task<decimal> CalcularValorLigacaoAsync(string dddOrigem, string dddDestino, int tempo);
        bool ehDoTipo(string plano);
        string Plano { get; }
    }

}