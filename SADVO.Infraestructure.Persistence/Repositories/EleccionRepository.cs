using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;
using SADVO.Infraestructure.Persistence.Contexts;

namespace SADVO.Infraestructure.Persistence.Repositories
{
    public class EleccionRepository : GenericRepository<Eleccion>, IEleccionRepository
    {
        private readonly SADVOContext _context;
        public EleccionRepository(SADVOContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<int>> ObtenerAniosDisponiblesAsync()
        {
            return await _context.Elecciones
     .Select(e => e.Fecha.Year)
     .Distinct()
     .OrderByDescending(a => a)
     .ToListAsync();
        }


    }
}
