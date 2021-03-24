using System.Collections.Generic;
using tel_api.Domain.Models;

namespace tel_api.Domain.Services
{
    public interface ILocalidadeService
    {
        IEnumerable<string> ListarLocalidades();
    }

    public class LocalidadeService : ILocalidadeService
    {
        public IEnumerable<string> ListarLocalidades()
        {
            return Localidade.Localidades;
        }
    }
}