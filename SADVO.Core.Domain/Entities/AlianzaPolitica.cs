using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class AlianzaPolitica
    {
        public int Id { get; set; }
        public required int PartidoSolicitanteId { get; set; }
        public int PartidoDestinoId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public required int Estado { get; set; } // "Pendiente", "Aceptada", "Rechazada"

        public PartidoPolitico PartidoSolicitante { get; set; } = null!;
        public PartidoPolitico PartidoDestino { get; set; } = null!;
    }

}
