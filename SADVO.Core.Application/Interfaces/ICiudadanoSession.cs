using SADVO.Core.Application.ViewModels;

namespace SADVO.Middleware
{
    public interface ICiudadanoSession
    {
        CiudadanoViewModel? GetCiudadanoSession();
    }
}