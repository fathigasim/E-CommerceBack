
using MediatR;


namespace EcommerceApplication.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }

    //    public record RegisterCommand(
    //    string FirstName,
    //    string LastName,
    //    string Email,
    //    string Password,
    //    string ConfirmPassword
    //) : IRequest<Result<AuthResponseDto>>;
}
