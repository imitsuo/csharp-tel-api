using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using tel_api.Domain.Models;
using tel_api.Domain.Services;

namespace tel_api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class SimuladorCustoLigacaoController : ControllerBase
    {
        private readonly ISimuladorTarifaService _simuladorService;

        public SimuladorCustoLigacaoController(ISimuladorTarifaService simuladorService)
        {
            _simuladorService = simuladorService;
        }


        [HttpGet()]
        [SwaggerOperation(Summary="Simula o Custo de uma ligacao com Plano Fale Mais e sem o Plano Fale Mais.", 
                          Description=@"plano=FALE_MAIS_30 dddOrigem=011 dddDestino=016 tempo=20")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna o Custo de uma ligacao com Plano Fale Mais e sem o Plano Fale Mais.", typeof(ComparativoCustoLigacao))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Quando um DDD ou plano informado não é suportado.", typeof(ComparativoCustoLigacao))]
        public async Task<ComparativoCustoLigacao> SimularCustoLigacao(
            [SwaggerParameter(Description="Plano", Required=true)] string plano, 
            [SwaggerParameter(Description="DDD de Origem", Required=true)] string dddOrigem, 
            [SwaggerParameter(Description="DDD de Destino", Required=true)] string dddDestino, 
            [SwaggerParameter(Description="Tempo da ligação em Minutos", Required=true)] int tempo)
        {
            var simulacao = new SimulacaoCustoLigacao();
            simulacao.DddDestino = dddDestino;
            simulacao.DddOrigem = dddOrigem;
            simulacao.Plano = plano;
            simulacao.Tempo = tempo;

            return await _simuladorService.CompararPlanoComTarifaFixaAsync(simulacao);
        }
    }
}