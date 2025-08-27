using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.Dtos
{
    public class EleccionResumenDTO
    {
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public int CantPartidos { get; set; }
        public int CantPuestos { get; set; }
        public bool EstaFinalizada { get; set; }
    }

}