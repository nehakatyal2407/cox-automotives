using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_server.Interfaces;
using api_server.Models;
using api_server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealsController : ControllerBase
    {
        private readonly ILogger<DealsController> _logger;

        public DealsController(ILogger<DealsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Deal> Get()
        {
            IExcelService ex = new ExcelService();
            return ex.LoadCsvFile("C:\\Users\\neegarg2\\Desktop\\Neha\\job hunt\\cox\\Dealertrack-CSV-Example.csv").ToArray();
        }

        [HttpGet("top")]
        public Object GetTopDeals()
        {
            IExcelService ex = new ExcelService();
            return ex.EvaluateTopSellingCars();
        }
    }
}
