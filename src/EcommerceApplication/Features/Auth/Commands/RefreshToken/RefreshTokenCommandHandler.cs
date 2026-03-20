
using EcommerceApplication.Interfaces;
using MediaRTutorialApplication.Interfaces;
using MediatR;



namespace EcommerceApplication.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public RefreshTokenCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _authenticationService.ValidateRefreshToken(request.UserId, request.RefreshToken);

            if (user == null)
            {
                return new RefreshTokenResponse
                {
                    Succeeded = false,
                    Error = "Invalid refresh token"
                };
            }

            var (accessToken, refreshToken) = await _authenticationService.GenerateTokens(user);

            return new RefreshTokenResponse
            {
                Succeeded = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
