using Microsoft.AspNetCore.Http;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Enums;

namespace SADVO.Middleware
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel? userViewModel = _httpContextAccessor.HttpContext?
                .Session.Get<UserViewModel>("User");

            if (userViewModel == null)
            {
                return false;
            }
            return true;
        }

        public UserViewModel? GetUserSession()
        {
            UserViewModel? userViewModel = _httpContextAccessor.HttpContext?
                .Session.Get<UserViewModel>("User");

            if (userViewModel == null)
            {
                return null;
            }
            return userViewModel;
        }

        public bool isAdmin()
        {
            UserViewModel? userViewModel = _httpContextAccessor.HttpContext?
                .Session.Get<UserViewModel>("User");

            if (userViewModel == null)
            {
                return false;
            }
            return userViewModel.Rol == (int)Role.ADMIN;
        }
    
}
}
