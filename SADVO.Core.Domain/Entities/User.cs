using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Nombre { get; set; } = null!;
        public required string Apellido { get; set; } = null!;
        public required string Email { get; set; } = null!;
        public required string NombreUsuario { get; set; } = null!;
        public required string ContrasenaHash { get; set; } = null!;
        public required int Rol { get; set; }  // "Administrador", "Dirigente"
        public required bool EstaActivo { get; set; } = true;
        public DirigentePolitico? DirigentePolitico { get; set; }
    }
}
