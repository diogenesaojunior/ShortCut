using FluentValidation;
using SisGerenciador.src.Models;
using System;

namespace SisGerenciador.Models.Validations
{
   
    public class AlunoValidator : AbstractValidator<Aluno>
    {
        public AlunoValidator()
        {
            RuleFor(c => c.DtNasc)
            .Must(MenorQueHoje).WithMessage("Data inválida");

            RuleFor(c => c.DtMatricula)
            .Must(MenorQueHoje).WithMessage("Data inválida");

        }

        private bool MenorQueHoje(DateTime dtNasc)
        {
            return dtNasc < DateTime.Now;
        }
    }
}
