using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.ViewModels
{
    public class DeletePuestoElectivoViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; } = null!;


    }

}
