using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api_server.Interfaces;
using api_server.Models;
using api_server.Services;
using Microsoft.AspNetCore.Http;
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
        // [Consumes("application/vnd.ms-excel")]
        public ActionResult<List<Deal>> PostAutoDeals()
        {
            try
            {
                var postedFile = Request.Form.Files[0];
                if (postedFile.Length > 0)
                {
                    _service.LoadCsvFile(postedFile);
                    return CreatedAtAction(nameof(PostAutoDeals),
                        new PostDealResponse
                        {
                            Deals = _service.getAllDeals(),
                            TopDeals = _service.EvaluateTopSellingCars()
                        });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error While Uploading Dealer Tracker File");
            }
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
