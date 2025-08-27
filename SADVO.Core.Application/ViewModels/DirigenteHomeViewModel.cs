using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class DirigenteHomeViewModel
    {
        public required int CandidatosActivos { get; set; }
        public required int CandidatosInactivos { get; set; }
        public required int CantidadAlianzas { get; set; }
        public required int CantidaSolicitudesAlianzas { get; set; }
        public required PartidoPoliticoViewModel PartidoPolitico { get; set; }
        public required int CandidatosAsignados { get; set; }
    }


}
