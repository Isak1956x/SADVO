using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.ViewModels
{
    public class AdminHomeViewModel
    {
        public List<int> Años = new List<int>();
        public List<EleccionViewModel> Elecciones = new List<EleccionViewModel>();
        public int añoSelecionado { get; set;}

    }
}
