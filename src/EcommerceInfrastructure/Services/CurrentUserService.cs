using MediaRTutorialApplication.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfrastructure.Services
{
    
        public class CurrentUserService : ICurrentUserService
        {
            public string? UserId { get; }
            public string? Email { get; }
            public bool IsAuthenticated { get; }

            public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            {
                var httpContext = httpContextAccessor.HttpContext;
                var user = httpContext?.User;

                // Cache all values immediately
                IsAuthenticated = user?.Identity?.IsAuthenticated ?? false;

                if (IsAuthenticated)
                {
                    UserId = user!.FindFirstValue(ClaimTypes.NameIdentifier);
                    Email = user.FindFirstValue(ClaimTypes.Email);
                }
            }
        }
    }
