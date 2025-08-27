using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace SADVO.Core.Application.ViewModels
{
    public class UpdateCandidatoViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe entrar un Nombre")]
        public required string Nombre { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe entrar un Apellido")]
        public required string Apellido { get; set; }
        public  IFormFile? FotoUrl { get; set; }


    }

}
