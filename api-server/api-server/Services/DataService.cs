using System;
using System.Collections.Generic;
using System.IO;
using api_server.Models;
using api_server.Util;
using api_server.Interfaces;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace api_server.Services
{
    public class DataService: IDataService
    {

        private readonly ILogger<DataService> _logger;
        private readonly DealsContext _context;

        public DataService(ILogger<DataService> logger, DealsContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async void LoadCsvFile(IFormFile postedFile)
        {
            using (var sr = new StreamReader(postedFile.OpenReadStream()))
            {
                string[] read;
                sr.ReadLine();
                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();
                    read = CSVLineProcessor.parseLine(line);
                    Deal dealtobeUploaded = CreateDealInstance(read);
                    Deal deal = await _context.Deals.FindAsync(dealtobeUploaded.DealNumber);
                    if(deal == null)
                    {
                        _context.Add(dealtobeUploaded);
                    }
                    
                }

                await _context.SaveChangesAsync();
            }
        }

        public List<Deal> getAllDeals()
        {
            return _context.Deals.ToList();
        }

        public Object EvaluateTopSellingCars()
        {
            return _context
                .Deals
                .AsEnumerable()
                .GroupBy(x => x.Vehicle)
                .OrderByDescending(x => x.Count())
                .Take(4)
                .Select(group => new
                {
                    name = group.Key
                })
                .ToList();
        }

        private static Deal CreateDealInstance(string[] read)
        {
            return new Deal()
            {
                DealNumber = int.Parse(read[0]),
                CustomerName = read[1],
                DealershipName = read[2],
                Vehicle = read[3],
                Price = Double.Parse(read[4]),
                Date = DateTime.Parse(read[5]).Date
            };
        }
    }
}
