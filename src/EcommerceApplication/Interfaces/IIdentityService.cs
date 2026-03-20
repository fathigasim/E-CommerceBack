using EcommerceApplication.Common.Settings;
using EcommerceApplication.DTOs;
using MediaRTutorialApplication.DTOs;


namespace MediaRTutorialApplication.Interfaces
{

    public interface IIdentityService
    {
        Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto);
        Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto);
    }
}
