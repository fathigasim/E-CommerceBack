using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest<LogoutResponse>
    {
        public string UserId { get; set; }
    }
}
