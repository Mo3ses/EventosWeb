using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventos.API.Data;
using Eventos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public EventosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _dataContext.Eventos;
                            
        }
        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            return _dataContext.Eventos.FirstOrDefault(e => e.EventoId == id);
                            
        }
        [HttpPost]
        public string Post()
        {
            return "ExemploPost";
        }
        [HttpPut("{id}")]
        public string Put()
        {
            return "ExemploPut";
        }
        [HttpDelete("{id}")]
        public string Delete()
        {
            return "ExemploDelete";
        }
    }
}
