﻿using System;
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

        // POST: api/deals
        [HttpPost]
        public ActionResult<List<Deal>> PostAutoDeals([FromBody] string path)
        {
            _service.LoadCsvFile(path);
            return CreatedAtAction(nameof(PostAutoDeals), _service.getAllDeals(), path);
        }

        // GET: api/deals
        [HttpGet]
        public ActionResult<List<Deal>> GetAutoDeals()
        {
            return _service.getAllDeals();
        }

        // GET: api/deals/top-vehicles
        [HttpGet("top-vehicles")]
        public Object GetTopDeals()
        {
            return _service.EvaluateTopSellingCars();
        }
    }
}
