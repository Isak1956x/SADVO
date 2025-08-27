using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class DirigentePoliticoViewModel
    {
        public int Id { get; set; }
        public required int UsuarioId { get; set; }
        public required int PartidoPoliticoId { get; set; }
        public UserViewModel Usuario { get; set; } = null!;
        public PartidoPoliticoViewModel PartidoPolitico { get; set; } = null!;

    }

}
