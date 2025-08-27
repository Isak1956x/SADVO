using Microsoft.AspNetCore.Http;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Enums;

namespace SADVO.Middleware
{
    public class CiudadanoSession : ICiudadanoSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CiudadanoSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        public CiudadanoViewModel? GetCiudadanoSession()
        {
            CiudadanoViewModel? ciudadanoVm = _httpContextAccessor.HttpContext?
                .Session.Get<CiudadanoViewModel>("Ciudadano");

            if (ciudadanoVm == null)
            {
                return null;
            }
            return ciudadanoVm;
        }



    }
}
