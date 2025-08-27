using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace SADVO.Core.Application.ViewModels
{
    public class SaveCandidatoViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Debe entrar un Nombre")]
        public required string Nombre { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe entrar un Apellido")]
        public required string Apellido { get; set; }
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Debe entrar una Foto")]
        public required IFormFile FotoUrl { get; set; }

    }

}
