using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; } 
        public required string NombreUsuario { get; set; }
        public required int Rol { get; set; }  // "Administrador", "Dirigente"
        public bool EstaActivo { get; set; } = true;
        public DirigentePoliticoViewModel? DirigentePolitico { get; set; }
    }
}
