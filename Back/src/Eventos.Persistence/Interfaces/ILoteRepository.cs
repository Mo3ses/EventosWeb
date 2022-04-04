using System.Threading.Tasks;
using Eventos.Domain;

namespace Eventos.Persistence.Interfaces
{
    public interface ILoteRepository
    {     
         Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
         Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId);

    }
}