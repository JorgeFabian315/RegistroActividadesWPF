﻿namespace RegistroDeActividades.Models.DTOS
{
    public class ActividadDTO
    {
        public int? Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string? Descripcion { get; set; }

        public DateOnly? FechaRealizacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public int Estado { get; set; }

        public string? Departamento { get; set; }

        public int DepartamentoId { get; set; }

        public string? Imagen { get; set; }
    }
}