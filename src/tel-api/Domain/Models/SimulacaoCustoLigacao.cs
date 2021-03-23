namespace tel_api.Domain.Models
{
    public class SimulacaoCustoLigacao
    {
        public string DddOrigem { get; set; }
        public string DddDestino { get; set; }
        public int Tempo { get; set; }
        public string PlanoFaleMais { get; set; }
        public decimal ValorComPlanoFaleMais { get; set; }
        public decimal ValorSemPlanoFaleMais { get; set; }
    }
}