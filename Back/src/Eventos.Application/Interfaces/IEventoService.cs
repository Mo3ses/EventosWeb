using System.Threading.Tasks;
using Eventos.Application.Dtos;

namespace Eventos.Application.Interfaces
{
    public interface IEventoService
    {
       Task<EventoDto> AddEvento(EventoDto model);  
       Task<EventoDto> UpdateEvento(int eventoId, EventoDto model);
       Task<bool> DeleteEvento(int eventoId);

       Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false);
       Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
       Task<EventoDto> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false);

    }
}