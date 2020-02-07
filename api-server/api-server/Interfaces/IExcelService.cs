using api_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_server.Interfaces
{
    interface IExcelService
    {
        List<Deal> LoadCsvFile(string filePath);
        Object EvaluateTopSellingCars();
    }
}
