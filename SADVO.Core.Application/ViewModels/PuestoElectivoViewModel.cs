using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.ViewModels
{
    public class PuestoElectivoViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; } = null!;
        public required string Descripcion { get; set; } = null!;
        public required bool EstaActivo { get; set; } = true;
        public  bool YaVotado { get; set; } = false;

        public ICollection<AsignacionPuestoViewModel> Asignaciones { get; set; } = new List<AsignacionPuestoViewModel>();

    }

}
