using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SADVO.Core.Application.ViewModels
{
    public class SaveEleccionViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="La Eleccion debe tener un nombre")]
        public required string Nombre { get; set; } = null!;
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La Eleccion debe tener una fecha de Realizacion")]
        public required DateTime Fecha { get; set; }
        public ICollection<VotoViewModel> Votos { get; set; } = new List<VotoViewModel>();


    }
}

