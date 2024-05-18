namespace RegistroDeActividades.Models.DTOS
{
    public class DepartamentoDTO
    {

        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Username { get; set; } = null!;

        public string? Password { get; set; } = null!;

        public int? IdSuperior { get; set; }

        //public  List<ActividadDTO> Actividades { get; set; } = new List<ActividadDTO>();

        public string? DepartamentoSuperior { get; set; }

    }
}
