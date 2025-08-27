using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class EleccionViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; } = null!;
        public required DateTime Fecha { get; set; }
        public required bool EstaFinalizada { get; set; } 
        public int? TotalPartidos { get; set; }
        public int? TotalPuestos { get; set; }
 public ICollection<VotoViewModel> Votos { get; set; } = new List<VotoViewModel>();


    }
}

