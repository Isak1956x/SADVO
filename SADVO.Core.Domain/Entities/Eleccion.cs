using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class Eleccion
    {
        public int Id { get; set; }
        public required string Nombre { get; set; } = null!;
        public required DateTime Fecha { get; set; }
        public required bool EstaFinalizada { get; set; }

        public ICollection<Voto> Votos { get; set; } = new List<Voto>();
        public List<Candidato> Candidatos { get; set; } = new List<Candidato>();


    }
}

