using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SisGerenciador.src.Models
{
    public class Docente : Usuario
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Endereço")]
        [StringLength(500)]
        public string Endereco { get; set; }

        [Display(Name = "Data de Matricula")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtMatricula { get; set; }

        public Instituicao Instituicao { get; set; }
        public int InstituicaoId { get; set; }

        public virtual ICollection<Turma> Turmas { get; set; }

    }
}
