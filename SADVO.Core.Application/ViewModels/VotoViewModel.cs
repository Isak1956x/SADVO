using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.ViewModels
{
    public class VotoViewModel
    {
        public required int Id { get; set; }
        public required int CiudadanoId { get; set; }
        public required int EleccionId { get; set; }
        public required int PuestoElectivoId { get; set; }
        public required int CandidatoId { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.UtcNow;

        public CiudadanoViewModel Ciudadano { get; set; } = null!;
        public EleccionViewModel Eleccion { get; set; } = null!;
        public PuestoElectivoViewModel PuestoElectivo { get; set; } = null!;
        public CandidatoViewModel? Candidato { get; set; }
    }

}
