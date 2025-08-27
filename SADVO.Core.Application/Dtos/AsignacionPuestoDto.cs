using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class AsignacionPuestoDto
    {
        public int Id { get; set; }
        public required int CandidatoId { get; set; }
        public required int PuestoElectivoId { get; set; }
        public required int PartidoPoliticoId { get; set; } // Partido al que representa en esta asignación

        public CandidatoDto Candidato { get; set; } = null!;
        public PuestoElectivoDto PuestoElectivo { get; set; } = null!;
        public PartidoPoliticoDto PartidoPolitico { get; set; } = null!;
    }

}
