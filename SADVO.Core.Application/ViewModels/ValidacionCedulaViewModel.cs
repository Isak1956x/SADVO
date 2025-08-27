using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SADVO.Core.Application.ViewModels
{
    public class validacionCedulaViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe entrar una Cedula")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "La cédula debe tener  11 caracteres.")]
        public required string Cedula { get; set; }

    }
}
