using System.Threading.Tasks;
using Eventos.Domain;

namespace Eventos.Persistence.Interfaces
{
    public interface IPalestrantesRepository
    {
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos);
         Task<Palestrante[]> GetAllPalestrantesAsync(string nome, bool includeEventos);
         Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos);

    }
}