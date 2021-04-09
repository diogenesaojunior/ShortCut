using FluentValidation;
using SisGerenciador.src.Models;
using System;

namespace SisGerenciador.Models.Validations
{
   
    public class DocenteValidator : AbstractValidator<Docente>
    {
        public DocenteValidator()
        {
            RuleFor(c => c.DtMatricula)
            .Must(MenorQueHoje).WithMessage("Data inválida");

        }

        private bool MenorQueHoje(DateTime dtNasc)
        {
            return dtNasc < DateTime.Now;
        }
    }
}
