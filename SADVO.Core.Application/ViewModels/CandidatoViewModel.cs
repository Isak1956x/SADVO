using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class CandidatoViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string FotoUrl { get; set; }
        public required bool EstaActivo { get; set; } = true;
        public required int PartidoPoliticoId { get; set; }
        public string? NombrePuesto { get; set; }

        public PartidoPoliticoViewModel? PartidoPolitico { get; set; }
        public ICollection<AsignacionPuestoViewModel> Asignaciones { get; set; } = new List<AsignacionPuestoViewModel>();
        public ICollection<VotoViewModel> Votos { get; set; } = new List<VotoViewModel>();

    }

}
