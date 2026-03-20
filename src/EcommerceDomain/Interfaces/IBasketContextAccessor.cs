using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Interfaces
{
    public interface IBasketContextAccessor
    {
        string? GetBasketId();
        void SetBasketId(string basketId);
        void ClearBasketId();
    }
}
