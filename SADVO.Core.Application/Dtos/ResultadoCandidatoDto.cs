using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.Dtos
{
    public class ResultadoCandidatoDTO
    {
        public string NombreCompletoCandidato { get; set; }
        public string PartidoNombre { get; set; }
        public int CantVotos { get; set; }
        public double Porcentaje { get; set; }
    }
}
