using System;
using System.Threading.Tasks;
using Eventos.Application.Interfaces;
using Eventos.Domain;
using Eventos.Persistence.Interfaces;

namespace Eventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IGeralRepository _geralRepository;
        private readonly IEventosRepository _eventosRepository;
        public EventoService(IGeralRepository geralRepository, IEventosRepository eventosRepository)
        {
            _eventosRepository = eventosRepository;
            _geralRepository = geralRepository;

        }
        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _geralRepository.Add<Evento>(model);
                if(await _geralRepository.SaveChangesAsync())
                {
                    return await _eventosRepository.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventosRepository.GetEventoByIdAsync(eventoId, false);
                if(evento == null) return null;

                model.Id = eventoId;

                _geralRepository.Update(model);
                if(await _geralRepository.SaveChangesAsync())
                {
                    return await _eventosRepository.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventosRepository.GetEventoByIdAsync(eventoId, false);
                if(evento == null) throw new Exception("Evento para delete não encontrado.");

                _geralRepository.Delete<Evento>(evento);
                return await _geralRepository.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosRepository.GetAllEventosAsync(includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosRepository.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventosRepository.GetEventoByIdAsync(EventoId ,includePalestrantes);
                if(evento == null) return null;

                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    
            }
        }

    }
}