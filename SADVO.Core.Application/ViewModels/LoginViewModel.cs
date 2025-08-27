using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.ViewModels
{
    public class LoginViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Debe entrar un UserName")]
        public required string Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Debe entra una Contraseña")]
        public required string Password { get; set; }
    }
}
