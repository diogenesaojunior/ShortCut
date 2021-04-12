using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SisGerenciador.src.Models
{
    public class Horario
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Dia da Semana")]
        [Required(ErrorMessage = "Dia da semana é obrigatório")]
        public int DiaSemana { get; set; }

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "Turno é obrigatório")]
        public string Turno { get; set; }

        public HoraAula HoraAula { get; set; }
        public int HoraAulaId { get; set; }

        public virtual ICollection<TurmaHorario> TurmaHorarios { get; set; }
        public virtual ICollection<Restricao> Restricoes { get; set; }
    }
}
