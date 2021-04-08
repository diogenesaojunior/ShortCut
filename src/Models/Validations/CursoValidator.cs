using FluentValidation;
using SisGerenciador.src.Models;
using System;

namespace SisGerenciador.Models.Validations
{
   
    public class CursoValidator : AbstractValidator<Curso>
    {
        public CursoValidator()
        {
            RuleFor(c => c.QtdPeriodo)
            .LessThan(customer => customer.LimiteQtdPeriodoCurricular).WithMessage("O limite precisa ser maior que quantidade de períodos");
        }
    }
}
