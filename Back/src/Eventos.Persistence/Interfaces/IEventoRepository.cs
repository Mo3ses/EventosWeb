using System.Threading.Tasks;
using Eventos.Domain;

namespace Eventos.Persistence.Interfaces
{
    public interface IEventoRepository
    {     
         Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
         Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
         Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes);

    }
}