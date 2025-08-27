using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class AsignacionPuestoViewModel
    {
        public int Id { get; set; }
        public required int CandidatoId { get; set; }
        public required int PuestoElectivoId { get; set; }
        public required int PartidoPoliticoId { get; set; } // Partido al que representa en esta asignación

        public CandidatoViewModel Candidato { get; set; } = null!;
        public PuestoElectivoViewModel PuestoElectivo { get; set; } = null!;
        public PartidoPoliticoViewModel PartidoPolitico { get; set; } = null!;
    }

}
