using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.ViewModels
{
    public class SaveVotoViewModel
    {
        public required int Id { get; set; }
    
        public required int CiudadanoId { get; set; }
        public required int EleccionId { get; set; }
        public required int PuestoElectivoId { get; set; }
        [Required(ErrorMessage = "Debe Elegir un Candidato")]
        public required int CandidatoId { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.UtcNow;

        public List<CandidatoViewModel>? Candidatos { get; set; }
    }

}
