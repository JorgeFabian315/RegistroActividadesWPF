using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroActividades.Models.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Rol { get; set; } = null!;

    }
}
