using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using tel_api.Domain.Models;

namespace tel_api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class SimuladorCustoLigacaoController : ControllerBase
    {
        [HttpGet()]
        [SwaggerOperation(Summary = "Simula o Custo de uma ligacao com Plano Fale Mais e sem o Plano Fale Mais.", Description = "asdd")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna o Custo de uma ligacao com Plano Fale Mais e sem o Plano Fale Mais.", typeof(SimulacaoCustoLigacao))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Quando um DDD ou plano informado não é suportado.", typeof(SimulacaoCustoLigacao))]
        public SimulacaoCustoLigacao SimularCustoLigacao(
            [SwaggerParameter(Description = "Plano", Required =true)] string plano, 
            [SwaggerParameter(Description = "DDD de Origem", Required =true)] string dddOrigem, 
            [SwaggerParameter(Description = "DDD de Destino", Required =true)] string dddDestino, 
            [SwaggerParameter(Description = "Tempo da ligação em Minutos", Required =true)] int tempo)
        {
            return new SimulacaoCustoLigacao();        
        }
    }
}