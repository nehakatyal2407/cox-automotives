using api_server.Interfaces;
using api_server.Models;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api_server.Test
{
    [TestFixture]
    public class APIServerUnitTest
    {
        private readonly IDataService _service;

        private readonly DealsContext _context;
        public object FoamFile { get; private set; }

        public APIServerUnitTest(IDataService service, DealsContext context)
        {
            _service = service;
            _context = context;
        }

        [Test]
        public void LoadCSVFileToInMemoryEFDB()
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\Dealertrack-CSV-Example.csv");
            FileStream basestream = new FileStream(path, FileMode.Open);
            var file = new FormFile(basestream, 0, basestream.Length, null, Path.GetFileName(basestream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };

            _service.LoadCsvFile(file);
            Assert.AreEqual(14, _context.Deals.ToList().Count());
        }

        [Test]
        public void TakeTopDeals()
        {
            Assert.IsNotNull(_service.EvaluateTopSellingCars());
        }

        [Test]
        public void FlushAllData()
        {

            _service.flushAllData();
            Assert.AreEqual(0, _context.Deals.ToList().Count());
        }

    }

}
