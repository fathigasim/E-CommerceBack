
using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Auth.Dtos;
using EcommerceApplication.Interfaces;
using EcommerceDomain.Entities;
using MediaRTutorialApplication.Interfaces;


using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace EcommerceApplication.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly JwtSettings _jwtSettings;

        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAuthenticationService authenticationService,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginResponse
                {
                    Succeeded = false,
                    Error = "Invalid credentials"
                };
            }

            if (!user.IsActive)
            {
                return new LoginResponse
                {
                    Succeeded = false,
                    Error = "Account is deactivated"
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                return new LoginResponse
                {
                    Succeeded = false,
                    Error = result.IsLockedOut ? "Account is locked" : "Invalid credentials"
                };
            }

            var (accessToken, refreshToken) = await _authenticationService.GenerateTokens(user);
            var roles = await _userManager.GetRolesAsync(user);

            user.LastLogin = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return new LoginResponse
            {
                Succeeded = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles.ToList()
                }
            };
        }
    }

    //public class LoginCommandHandler
    //: IRequestHandler<LoginCommand, Result<LoginResponse>>
    //{
    //    private readonly IIdentityService _identityService;

    //    public LoginCommandHandler(IIdentityService identityService)
    //    {
    //        _identityService = identityService;
    //    }

    //    public async Task<Result<LoginResponse>> Handle(
    //        LoginCommand request, CancellationToken cancellationToken)
    //    {
    //        return await _identityService.LoginAsync(
    //            new LoginDto(request.Email, request.Password));
    //    }
    //}

}
