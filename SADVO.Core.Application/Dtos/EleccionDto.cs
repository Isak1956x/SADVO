using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class EleccionDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required DateTime Fecha { get; set; }
        public required bool EstaFinalizada { get; set; }
        public ICollection<VotoDto> Votos { get; set; } = new List<VotoDto>();
        public List<CandidatoDto> Candidatos { get; set; } = new List<CandidatoDto>();


    }
}

