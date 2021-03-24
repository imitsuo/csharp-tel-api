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
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoComercializadoService _planoService;

        public PlanoController(IPlanoComercializadoService planoService)
        {
            _planoService = planoService;
        }


        [HttpGet()]
        [SwaggerOperation(Summary="Lista de Planos Disponiveis.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna Lista de Planos Disponiveis.", typeof(IEnumerable<string>))]        
        public IEnumerable<string> ListarPlanos()          
        {
            return _planoService.ListarPlanos();
        }
    }
}