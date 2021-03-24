using System.Collections.Generic;
using tel_api.Domain.Models;

namespace tel_api.Domain.Services
{
    public interface IPlanoComercializadoService
    {
        IEnumerable<string> ListarPlanos();
    }

    public class PlanoComercializadoService : IPlanoComercializadoService
    {
        public IEnumerable<string> ListarPlanos()
        {
            return Plano.Planos;
        }
    }
}