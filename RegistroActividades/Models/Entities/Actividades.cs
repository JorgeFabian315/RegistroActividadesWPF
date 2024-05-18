using SQLite;

namespace RegistroDeActividades.Models.Entities;

[Table("Actividades")]
public partial class Actividades
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Titulo { get; set; } = null!;
    [NotNull]
    public string? Descripcion { get; set; }
    [NotNull]
    public DateOnly? FechaRealizacion { get; set; }
    [NotNull]
    public int IdDepartamento { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public int Estado { get; set; }

    public virtual Departamentos IdDepartamentoNavigation { get; set; } = null!;
}
