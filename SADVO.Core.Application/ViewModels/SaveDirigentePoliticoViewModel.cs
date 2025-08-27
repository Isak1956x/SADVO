using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class SaveDirigentePoliticoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe entrar un Usuario")]
        public required int UsuarioId { get; set; }
        [Required(ErrorMessage = "Debe entrar un Partido")]
        public required int PartidoPoliticoId { get; set; }

    }

}
