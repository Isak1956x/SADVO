using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SADVO.Core.Domain.Interfaces;
using SADVO.Infraestructure.Persistence.Contexts;
using SADVO.Infraestructure.Persistence.Repositories;

namespace SADVO.Infraestructure.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceLayerIoc(this IServiceCollection services,  IConfiguration configuration)
        {
            #region Contexts
            var conecctionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SADVOContext>(options =>
                options.UseSqlServer(conecctionString,
                m => m.MigrationsAssembly(typeof(SADVOContext).Assembly.FullName)),
                ServiceLifetime.Transient);
            #endregion
            #region Repositories IOC
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICiudadanoRepository, CiudadanoRepository>();
            services.AddTransient<IPartidoPoliticoRepository, PartidoPoliticoRepository>();
            services.AddTransient<IEleccionRepository, EleccionRepository>();
            services.AddTransient<IPuestoElectivoRepository, PuestoElectivoRepository>();
            services.AddTransient<IVotoRepository, VotoRepository>();
            services.AddTransient<IDirgentePoliticoRepository, DirigentePoliticoRepository>();
            services.AddTransient<ICandidatoRepository, CandidatoRepository>();
            services.AddTransient<IAlianzaPoliticaRepository, AlianzaPoliticaRepository>();
            services.AddTransient<IAsignacionPuestoRepository, AsignacionPuestoRepository>();
            #endregion


        }

    }
}
