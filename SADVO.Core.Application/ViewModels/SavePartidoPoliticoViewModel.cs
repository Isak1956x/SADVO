using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SADVO.Core.Application.ViewModels
{
    public class SavePartidoPoliticoViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe Entrar un Nombre")]
        public required string Nombre { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe Entrar una Sigla")]
        public required string Siglas { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Descripcion { get; set; }
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "El Partido debe tener un Logo")]
        public IFormFile? LogoUrl { get; set; }

    }

}
