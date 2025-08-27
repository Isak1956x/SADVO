using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SADVO.Core.Application.ViewModels
{
    public class AlianzaPoliticaIndexViewModel
    {
public required List<AlianzaPoliticaViewModel> AlianzasPendientes { get; set; } 
        public required List<AlianzaPoliticaViewModel> AlianzasAceptadas { get; set; } 
        public required List<AlianzaPoliticaViewModel> AlianzasEnviadas {get; set; } 

    }

}
