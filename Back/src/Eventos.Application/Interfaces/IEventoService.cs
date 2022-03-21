using System.Threading.Tasks;
using Eventos.Domain;

namespace Eventos.Application.Interfaces
{
    public interface IEventoService
    {
       Task<Evento> AddEvento(Evento model);  
       Task<Evento> UpdateEvento(int eventoId, Evento model);
       Task<bool> DeleteEvento(int eventoId);

       Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
       Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
       Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false);

    }
}