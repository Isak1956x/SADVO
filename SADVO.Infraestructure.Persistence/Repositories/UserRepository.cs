using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;
using SADVO.Infraestructure.Persistence.Contexts;

namespace SADVO.Infraestructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SADVOContext _context;
        public UserRepository(SADVOContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            string hashedPassword = PasswordEncriptation.ComputeSha256Hash(password);
            User? user = await _context.Set<User>()
                .FirstOrDefaultAsync(u => u.NombreUsuario == username && u.ContrasenaHash == hashedPassword);

            return user;
        }
    }
}
