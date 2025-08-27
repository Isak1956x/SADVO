using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class AsignacionPuesto
    {
        public int Id { get; set; }
        public required int CandidatoId { get; set; }
        public required int PuestoElectivoId { get; set; }
        public required int PartidoPoliticoId { get; set; } 

        public Candidato Candidato { get; set; } = null!;
        public PuestoElectivo PuestoElectivo { get; set; } = null!;
        public PartidoPolitico PartidoPolitico { get; set; } = null!;
    }

}
