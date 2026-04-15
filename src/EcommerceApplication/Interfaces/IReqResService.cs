using EcommerceApplication.Common.EcommerceApplication.Common;
using EcommerceApplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Interfaces
{
    public interface IReqResService
    {
        Task<ReqResUserDto> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    }
}
