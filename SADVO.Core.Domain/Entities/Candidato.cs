using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class Candidato
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; } 
        public required string FotoUrl { get; set; }
        public required bool EstaActivo { get; set; } = true;
        public required int PartidoPoliticoId { get; set; }

        public PartidoPolitico? PartidoPolitico { get; set; }
        public ICollection<AsignacionPuesto> Asignaciones { get; set; } = new List<AsignacionPuesto>();
        public ICollection<Voto> Votos { get; set; } = new List<Voto>();
    

    } 


}
