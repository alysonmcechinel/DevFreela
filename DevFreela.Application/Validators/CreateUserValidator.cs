using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserInputModel>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email invalido");

            RuleFor(x => x.BirthDate)
                .Must(i => i < DateTime.Now.AddYears(-18))
                .WithMessage("Deve ser maior de idade.");
        }
    }
}
