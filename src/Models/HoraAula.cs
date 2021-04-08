using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SisGerenciador.src.Models
{
    public class HoraAula
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatória", AllowEmptyStrings = false)]
        [StringLength(1, ErrorMessage = "A descrição deve conter no máximo 1 caractere.")]
        public string Descricao { get; set; }

        [Display(Name = "Hora de Início")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{HH:mm}")]
        [Required(ErrorMessage = "Horário de início é obrigatório")]
        public DateTime DtInicio { get; set; }

        [Display(Name = "Hora de Fim")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{HH:mm}")]
        [Required(ErrorMessage = "Horário de fim é obrigatório")]
        public DateTime DtFim { get; set; }

        public virtual ICollection<Horario> Horarios { get; set; }
    }
}
