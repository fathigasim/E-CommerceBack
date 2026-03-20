using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Basket.DTOs
{
    public record BasketSummaryDto(int ItemCount, decimal Total);
}
