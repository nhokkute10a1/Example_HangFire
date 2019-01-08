using System;
using System.Collections.Generic;
using System.Text;

namespace RouteDelivery.OptimizationEngine
{
    public interface IOptimizationEngine
    {
        void OptimizeDeliveries(int requestID);
    }
}
