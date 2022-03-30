using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        StringLength(50, MinimumLength = 3, ErrorMessage = "Intervalo de 3 a 50 caracteres.")]
        public string Tema { get; set; }
        [Range(1, 120000, ErrorMessage ="Intervalo de pessoas permitido é entre 1 e 120.000")]
        public int QtdPessoas { get; set; } 
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem válida. (gif, jpg, jpeg, bmp ou png)")]
        public string ImagemURL { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        Phone(ErrorMessage = "O campo {0} está com numero inválido.")]
        public string Telefone { get; set; }   
        [Required(ErrorMessage = "O campo {0} é obrigatório."), EmailAddress(ErrorMessage = "O endereço de email é invalido.")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedeSociais { get; set; }
        public IEnumerable<PalestranteDto> PalestrantesEventos { get; set; }

    }
}