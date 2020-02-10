using api_server.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_server.Interfaces
{
    public interface IDataService
    {
        void LoadCsvFile(IFormFile postedFile);

        List<Deal> getAllDeals();

        object EvaluateTopSellingCars();

        void flushAllData();
    }
}
