using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class PuestoElectivoDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; } = null!;
        public required string Descripcion { get; set; } = null!;
        public required bool EstaActivo { get; set; } = true;
        public ICollection<AsignacionPuestoDto> Asignaciones { get; set; } = new List<AsignacionPuestoDto>();

    }

}
