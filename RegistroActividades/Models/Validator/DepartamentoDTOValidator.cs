using FluentValidation;
using RegistroDeActividades.Models.DTOS;
using RegistroDeActividades.Models.Entities;
namespace RegistroDeActividades.Models.Validator
{
    public class DepartamentoDTOValidator : AbstractValidator<DepartamentoDTO>
    {

        public DepartamentoDTOValidator(IEnumerable<Departamentos> departamentos)
        {

            RuleFor(RuleFor => RuleFor.Nombre)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("El nombre del departamento es requerido")
                .MaximumLength(50)
                .WithMessage("El nombre del departamento no puede tener más de 50 caracteres")
                .Must(x =>
                {
                    return !DepartamentoExistsNombre(departamentos, x);
                })
                .WithMessage("El nombre del departamento ya existe en la base de datos");


            RuleFor(RuleFor => RuleFor.Username)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("El username del departamento es requerido")
                .MaximumLength(50)
                .WithMessage("El username del departamento no puede tener más de 50 caracteres");

            RuleFor(RuleFor => RuleFor.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("El password del departamento es requerido")
                .MaximumLength(128)
                .WithMessage("El password del departamento no puede tener más de 128 caracteres");


            RuleFor(x => x.IdSuperior)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("El id del departamento superior es requerido")
                .GreaterThan(0)
                .WithMessage("El id del departamento superior debe ser mayor a 0");
        }


        public bool DepartamentoExistsNombre(IEnumerable<Departamentos> departamentos, string nombre)
        {
            return departamentos.Any(x => x.Nombre == nombre);
        }


    }
}
