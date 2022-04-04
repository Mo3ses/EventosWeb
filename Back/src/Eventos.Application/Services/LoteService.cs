using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eventos.Application.Dtos;
using Eventos.Application.Interfaces;
using Eventos.Domain;
using Eventos.Persistence.Interfaces;

namespace Eventos.Application.Services
{
    public class LoteService : ILoteService
    {
        private readonly IGeralRepository _geralRepository;
        private readonly ILoteRepository _loteRepository;
        private readonly IMapper _mapper;
        public LoteService(IGeralRepository geralRepository, ILoteRepository loteRepository, IMapper mapper)
        {
            _loteRepository = loteRepository;
            _geralRepository = geralRepository;
            _mapper = mapper;

        }
        public async Task AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoId;

                _geralRepository.Add<Lote>(lote);

                await _geralRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> SaveLote(int eventoId, LoteDto[] models)
        {
            try
            {
                
                var lotes = await _loteRepository.GetLotesByEventoIdAsync(eventoId);
                if(lotes == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
                        model.EventoId = eventoId;

                        _mapper.Map(model, lote);

                        _geralRepository.Update<Lote>(lote);

                        await _geralRepository.SaveChangesAsync();
                    }
                }
                    var loteRetorno = await _loteRepository.GetLotesByEventoIdAsync(eventoId);
                    return _mapper.Map<LoteDto[]>(loteRetorno);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdsAsync(eventoId, loteId);
                if(lote == null) throw new Exception("Lote para delete não encontrado.");

                _geralRepository.Delete<Lote>(lote);
                return await _geralRepository.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _loteRepository.GetLotesByEventoIdAsync(eventoId);
                if(lotes == null) return null;

                var resultado = _mapper.Map<LoteDto[]>(lotes);

                return resultado;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdsAsync(eventoId, loteId);
                if(lote == null) return null;
                
                var resultado = _mapper.Map<LoteDto>(lote);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    
            }
        }
    }
}