using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.ViewModels
{
    public class PartidoPoliticoViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Siglas { get; set; }
        public required string? Descripcion { get; set; }
        public required string LogoUrl { get; set; }
        public required bool EstaActivo { get; set; } = true;
        public ICollection<CandidatoViewModel> Candidatos { get; set; } = new List<CandidatoViewModel>();
        public ICollection<AlianzaPoliticaViewModel> AlianzasEnviadas { get; set; } = new List<AlianzaPoliticaViewModel>();
        public ICollection<AlianzaPoliticaViewModel> AlianzasRecibidas { get; set; } = new List<AlianzaPoliticaViewModel>();

    }

}
