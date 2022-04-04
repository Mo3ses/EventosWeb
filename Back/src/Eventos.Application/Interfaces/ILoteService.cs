using System.Threading.Tasks;
using Eventos.Application.Dtos;

namespace Eventos.Application.Interfaces
{
    public interface ILoteService
    {
       Task<LoteDto[]> SaveLote(int eventoId, LoteDto[] models);
       Task<bool> DeleteLote(int eventoId, int loteId);

       Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
       Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId);


    }
}