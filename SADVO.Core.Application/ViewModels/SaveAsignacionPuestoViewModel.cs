using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class SaveAsignacionPuestoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe elegir un Candidato")]
        public required int CandidatoId { get; set; }
        [Required(ErrorMessage = "Debe elegir un Puesto")]
        public required int PuestoElectivoId { get; set; }
        public int? PartidoPoliticoId { get; set; } 

    }

}
