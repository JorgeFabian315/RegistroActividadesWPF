using SQLite;

namespace RegistroDeActividades.Models.Entities;

[Table("Actividades")]
public partial class Actividades
{
    [PrimaryKey]
    public int Id { get; set; }

    [NotNull]
    public string Titulo { get; set; } = null!;
    [NotNull]
    public string? Descripcion { get; set; }
    [NotNull]
    public string? FechaRealizacion { get; set; } 
    public int IdDepartamento { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public int Estado { get; set; }
    [NotNull]
    public string NombreDepartamento { get; set; } = string.Empty;

    //[Ignore]
    //public virtual Departamentos IdDepartamentoNavigation { get; set; } = null!;

    public string? Imagen { get; set; }

}
