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
    public class LocalidadeController : ControllerBase
    {
        private readonly ILocalidadeService _localidadeService;

        public LocalidadeController(ILocalidadeService localidadeService)
        {
            _localidadeService = localidadeService;
        }


        [HttpGet()]
        [SwaggerOperation(Summary="Lista de Localidades (ddd)")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna uma lista de localidades (ddd).", typeof(List<string>))]
        public IEnumerable<string> ListarLocalidades()
        {
            return _localidadeService.ListarLocalidades();
        }
    }
}