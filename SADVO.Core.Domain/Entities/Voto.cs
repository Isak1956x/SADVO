using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class Voto
    {
        public required int Id { get; set; }
        public required int CiudadanoId { get; set; }
        public required int EleccionId { get; set; }
        public required int PuestoElectivoId { get; set; }
        public required int CandidatoId { get; set; } 
        public DateTime FechaHora { get; set; } = DateTime.UtcNow;

        public Ciudadano Ciudadano { get; set; } = null!;
        public Eleccion Eleccion { get; set; } = null!;
        public PuestoElectivo PuestoElectivo { get; set; } = null!;
        public Candidato? Candidato { get; set; }
    }

}
