using System.Collections.Generic;

namespace Eventos.Application.Dtos
{
    public class PalestranteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobre { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<RedeSocialDto> RedeSociais { get; set; }

        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}