using api_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_server.Interfaces
{
    public interface IDataService
    {
        void LoadCsvFile(string filePath);

        List<Deal> getAllDeals();

        Object EvaluateTopSellingCars();
    }
}
