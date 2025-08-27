using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SADVO.Core.Application.ViewModels
{
    public class SaveAlianzaPoliticaViewModel
    {
        public int Id { get; set; }
        public required int PartidoSolicitanteId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe agregar un partido como destino")]
        public int PartidoDestinoId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public required int Estado { get; set; } 

    }

}
