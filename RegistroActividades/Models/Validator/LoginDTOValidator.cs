using FluentValidation;
using RegistroDeActividades.Models.DTOS;

namespace RegistroDeActividades.Models.Validator
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {

        public LoginDTOValidator()
        {

            RuleFor(RuleFor => RuleFor.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El username es requerido")
                .MaximumLength(50)
                .WithMessage("El username no puede tener más de 50 caracteres");


            RuleFor(RuleFor => RuleFor.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El password es requerido")
                .MaximumLength(128)
                .WithMessage("El password no puede tener más de 128 caracteres");

        }

    }
}
