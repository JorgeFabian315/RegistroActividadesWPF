using FluentValidation;
using RegistroDeActividades.Models.DTOS;
using RegistroDeActividades.Models.Entities;

namespace RegistroDeActividades.Models.Validator
{
    public class ActividadDTOValidator : AbstractValidator<ActividadDTO>
    {

        public ActividadDTOValidator(IEnumerable<Departamentos> departamentos)
        {
            RuleFor(RuleFor => RuleFor.Titulo)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("El titulo de la actividad es requerido")
                .MaximumLength(100)
                .WithMessage("El titulo de la actividad no puede tener más de 100 caracteres");


            RuleFor(RuleFor => RuleFor.Descripcion)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("La descripción de la actividad es requerida")
                .MaximumLength(500)
                .WithMessage("La descripción de la actividad no puede tener más de 500 caracteres");

            RuleFor(x => x.ImagenDecodificada)
                .NotEmpty() 
                .NotNull()
                .WithMessage("La imagen es requerida");


        }

        public bool DepartamentoExistsNombre(IEnumerable<Departamentos> departamentos, string nombre)
        {
            nombre = nombre.ToLower();
            return departamentos.Any(d => d.Nombre.ToLower() == nombre);
        }

        public bool DepartamentoExistsId(IEnumerable<Departamentos> departamentos, int id)
        {
            return departamentos.Any(d => d.Id == id);
        }


    }
}
