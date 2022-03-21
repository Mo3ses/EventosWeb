using System.Threading.Tasks;
using Eventos.Domain;

namespace Eventos.Persistence.Interfaces
{
    public interface IGeralRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         void DeleteRange<T>(T[] entity) where T: class;

         Task<bool> SaveChangesAsync();
    }
}