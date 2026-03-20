using EcommerceDomain.Entities;
using MediaRTutorialDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GenerateJwtToken(ApplicationUser user);
        string GenerateRefreshToken();
        Task<(string AccessToken, string RefreshToken)> GenerateTokens(ApplicationUser user);
        Task<ApplicationUser> ValidateRefreshToken(string userId, string refreshToken);
    }
}
