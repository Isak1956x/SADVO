using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.ViewModels
{
    public class SavePuestoElectivoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe Entra un Nombre de Puesto")]
        [DataType(DataType.Text)]
        public required string Nombre { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Debe Entra una Descripcion")]
        public required string Descripcion { get; set; }

    }

}
