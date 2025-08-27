using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class AlianzaPoliticaViewModel
    {
        public int Id { get; set; }
        public required int PartidoSolicitanteId { get; set; }
        public int PartidoDestinoId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public required int Estado { get; set; } 

        public PartidoPoliticoViewModel PartidoSolicitante { get; set; } = null!;
        public PartidoPoliticoViewModel PartidoDestino { get; set; } = null!;
    }

}
