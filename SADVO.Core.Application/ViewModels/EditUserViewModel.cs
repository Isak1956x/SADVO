using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Debe entrar un Nombre")]
        [DataType(DataType.Text)]
        public required string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Debe entrar un Apellido")]
        [DataType(DataType.Text)]
        public required string Apellido { get; set; } = null!;
        [Required(ErrorMessage = "Debe entrar un Email")]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; } = null!;
        [Required(ErrorMessage = "Debe entrar un Nombre de Usuario")]
        [DataType(DataType.Text)]
        public required string NombreUsuario { get; set; } = null!;
        [DataType(DataType.Password)]
        public  string? Contrasena { get; set; } = null!;
        [Compare(nameof(Contrasena), ErrorMessage = "La Contraseña no coincide")]
        [DataType(DataType.Password)]
        public  string? ContrasenaConfirmacion { get; set; } = null!;
    }
}
 