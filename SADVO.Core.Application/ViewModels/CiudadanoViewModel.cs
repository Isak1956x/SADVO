using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class CiudadanoViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string DocumentoIdentidad { get; set; }
        public required bool EstaActivo { get; set; } = true;

        public ICollection<VotoViewModel> Votos { get; set; } = new List<VotoViewModel>();

    }

}
