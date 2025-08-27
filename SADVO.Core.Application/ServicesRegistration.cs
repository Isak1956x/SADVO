using Microsoft.Extensions.DependencyInjection;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.Services;

namespace SADVO.Core.Application
{
    public static class ServicesRegistration
    {
        //Extension method - Decorator pattern
        public static void AddApplicationLayerIoc(this IServiceCollection services)
        {    
            #region Services IOC
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IPuestoElectivoServices, PuestoElectivoServices>();
            services.AddTransient<IPartidoPoliticoServices, PartidoPoliticoServices>();
            services.AddTransient<ICiudadanoServices, CiudadanoServices>();
            services.AddTransient<IDirigentePoliticoServices, DirigentePoliticoServices>();
            services.AddTransient<ICandidatoServices, CandidatoServices>();
            services.AddTransient<IAsignacionPuestoServices, AsignacionPuestoServices>();
            services.AddTransient<IEleccionServices, EleccionServices>();
            services.AddTransient<IAlianzaPoliticaServices, AlianzasPoliticasServices>();
            services.AddTransient<IVotoServices, VotoServices>();
            #endregion
        }

    }
}
