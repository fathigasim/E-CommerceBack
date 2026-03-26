using EcommerceApplication.Common.Settings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Auth.Commands.UserManagement.ResetPassword
{
   
   public class ForegotPasswordCommand : IRequest<Result<string>>
    {
       
        public string Email { get; set; }
    }
}
