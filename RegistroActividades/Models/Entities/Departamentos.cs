﻿using System;
using System.Collections.Generic;
using SQLite;

namespace RegistroDeActividades.Models.Entities;

[Table("Departamentos")]
public partial class Departamentos
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Nombre { get; set; } = null!;

    [NotNull]
    public string Username { get; set; } = null!;

    [NotNull]
    public string Password { get; set; } = null!;

    public int? IdSuperior { get; set; }

    public virtual ICollection<Actividades> Actividades { get; set; } = new List<Actividades>();

    public virtual Departamentos? IdSuperiorNavigation { get; set; }

    public virtual ICollection<Departamentos> InverseIdSuperiorNavigation { get; set; } = new List<Departamentos>();
}
