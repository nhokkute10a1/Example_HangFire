using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RouterDelivery.Services.IServices
{
    public interface IOptimizationEngineServices
    {
        List<OptimizationRequestViewModel> GetAll();
        OptimizationRequestViewModel GetEdit(int id);
        void Insert(InsertOptimizationRequestViewModel dto, out bool Status);
        void Update(OptimizationRequestViewModel dto, out bool Status);
        void Delete(int id, out bool Status);
        OptimizationRequestViewModel GetIdLasted();
    }
}
