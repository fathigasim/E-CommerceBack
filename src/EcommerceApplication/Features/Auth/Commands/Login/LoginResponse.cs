

using EcommerceApplication.Features.Auth.Dtos;

namespace EcommerceApplication.Features.Auth.Commands.Login
{
    public class LoginResponse
    {
        public bool Succeeded { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public UserDto User { get; set; }
        public string Error { get; set; }
    }
}
