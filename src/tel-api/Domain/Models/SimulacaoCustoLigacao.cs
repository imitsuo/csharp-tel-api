namespace tel_api.Domain.Models
{
    public class SimulacaoCustoLigacao
    {
        public string DddOrigem { get; set; }
        public string DddDestino { get; set; }
        public int Tempo { get; set; }
        public string Plano { get; set; }
    }
}