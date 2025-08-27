using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;
using SADVO.Infraestructure.Persistence.Contexts;

namespace SADVO.Infraestructure.Persistence.Repositories
{
    public class CiudadanoRepository : GenericRepository<Ciudadano>, ICiudadanoRepository
    {
        public CiudadanoRepository(SADVOContext context) : base(context)
        {
        }
    }
}
