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
        private readonly IDataService _service;

        public DealsController(ILogger<DealsController> logger, IDataService service)
        {
            _logger = logger;
            _service = service; 
        }

        [HttpGet]
        public IEnumerable<Deal> Get()
        {
             _service.LoadCsvFile("C:\\Users\\neegarg2\\Desktop\\Neha\\job hunt\\cox-auto\\cox-automotives\\Dealertrack-CSV-Example.csv");
            return _service.getAllDeals();
        }

        [HttpGet("top")]
        public Object GetTopDeals()
        {
            return _service.EvaluateTopSellingCars();
        }
    }
}
