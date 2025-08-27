using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class PartidoPoliticoDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Siglas { get; set; }
        public required string? Descripcion { get; set; }
        public required string LogoUrl { get; set; } 
        public required bool EstaActivo { get; set; } = true;
        public ICollection<CandidatoDto> Candidatos { get; set; } = new List<CandidatoDto>();
        public ICollection<AlianzaPoliticaDto> AlianzasEnviadas { get; set; } = new List<AlianzaPoliticaDto>();
        public ICollection<AlianzaPoliticaDto> AlianzasRecibidas { get; set; } = new List<AlianzaPoliticaDto>();

    }

}
