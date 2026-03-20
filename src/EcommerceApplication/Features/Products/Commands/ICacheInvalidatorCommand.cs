using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Products.Commands
{
    public interface ICacheInvalidatorCommand
    {
        string[] CacheKeys { get; }
    }
}
