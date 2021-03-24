namespace tel_api.Domain.Models
{
    public class TarifaFixa
    {
        public long Id { get; set; }
        public string CodigoIdentificador { get; set; }
        public string DddOrigem { get; set; }
        public string DddDestino { get; set; }
        public decimal Valor { get; set; }
    }
}