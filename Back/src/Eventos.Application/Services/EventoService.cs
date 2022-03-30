using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Eventos.Application.Dtos;
using Eventos.Application.Interfaces;
using Eventos.Domain;
using Eventos.Persistence.Interfaces;

namespace Eventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IGeralRepository _geralRepository;
        private readonly IEventosRepository _eventosRepository;
        private readonly IMapper _mapper;
        public EventoService(IGeralRepository geralRepository, IEventosRepository eventosRepository, IMapper mapper)
        {
            _eventosRepository = eventosRepository;
            _geralRepository = geralRepository;
            _mapper = mapper;

        }
        public async Task<EventoDto> AddEvento(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _geralRepository.Add<Evento>(evento);
                if(await _geralRepository.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventosRepository.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                
                var evento = await _eventosRepository.GetEventoByIdAsync(eventoId, false);
                if(evento == null) return null;

                model.Id = eventoId;

                _mapper.Map(model, evento);

                _geralRepository.Update<Evento>(evento);
                if(await _geralRepository.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventosRepository.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
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

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosRepository.GetAllEventosAsync(includePalestrantes);
                if(eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosRepository.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if(eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventosRepository.GetEventoByIdAsync(EventoId ,includePalestrantes);
                if(evento == null) return null;
                
                var resultado = _mapper.Map<EventoDto>(evento);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    
            }
        }

    }
}