using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class CandidatoDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; } 
        public required string FotoUrl { get; set; }
        public required bool EstaActivo { get; set; } = true;
        public required int PartidoPoliticoId { get; set; }

        public PartidoPoliticoDto? PartidoPolitico { get; set; }
        public ICollection<AsignacionPuestoDto> Asignaciones { get; set; } = new List<AsignacionPuestoDto>();
        public ICollection<VotoDto> Votos { get; set; } = new List<VotoDto>();
        public PuestoElectivoDto Puesto { get; set; }

    }

}
