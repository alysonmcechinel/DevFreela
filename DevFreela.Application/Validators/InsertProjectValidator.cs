using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Commands.InsertProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
    {
        public InsertProjectValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage("Não pode ser vazio")
                .MaximumLength(50)
                    .WithMessage("Tamanho não pode ser maior 50");

            RuleFor(x => x.TotalCost)
                .GreaterThanOrEqualTo(1000)
                .WithMessage("O projeto deve custar pelo menos milzao");
        }
    }
}
