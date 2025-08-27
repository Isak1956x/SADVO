using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Domain.Entities
{
    public class DirigentePolitico
    {
        public int Id { get; set; }
        public required int UsuarioId { get; set; }
        public required int PartidoPoliticoId { get; set; }
        public User Usuario { get; set; } = null!;
        public PartidoPolitico PartidoPolitico { get; set; } = null!;

    }

}
