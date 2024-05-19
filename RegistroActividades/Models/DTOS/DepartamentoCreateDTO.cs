﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroActividades.Models.DTOS
{
    public class DepartamentoCreateDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int IdSuperior { get; set; }
    }
}
