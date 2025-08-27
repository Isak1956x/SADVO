using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class SaveCiudadanoViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe Entrar Un Nombre")]
        public required string Nombre { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe Entrar Un Apellido")]
        public required string Apellido { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe Entrar Un Email")]
        public required string Email { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe Entrar Un Documento De Indentidad")]
        public required string DocumentoIdentidad { get; set; }
        public required bool EstaActivo { get; set; } = true;

        public ICollection<VotoViewModel> Votos { get; set; } = new List<VotoViewModel>();

    }

}
