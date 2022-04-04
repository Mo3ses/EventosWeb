using System.Linq;
using System.Threading.Tasks;
using Eventos.Domain;
using Eventos.Persistence.Data;
using Eventos.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eventos.Persistence
{
    public class LoteRepository : ILoteRepository
    {
        private readonly EventosDataContext _context;
        public LoteRepository(EventosDataContext context)
        {
            _context = context;
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId && lote.Id == loteId);
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId);
            
            return await query.ToArrayAsync();
        }
    }
}