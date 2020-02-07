using System;
using System.Collections.Generic;
using System.IO;
using api_server.Models;
using api_server.Util;
using api_server.Interfaces;
using api_server.Repository;
using System.Linq;

namespace api_server.Services
{
    public class ExcelService:IExcelService
    {
        DealsInMemoryRepository repo = new DealsInMemoryRepository();
        public List<Deal> LoadCsvFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string[] read;
                sr.ReadLine();
                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();
                    read = CSVLineProcessor.parseLine(line);
                    repo.Add(CreateDealInstance(read));
                }

                return repo.GetAll();
            }
        }

        public Object EvaluateTopSellingCars()
        {
            return repo.GetAll()
                .GroupBy(x => x.Vehicle)
                .OrderByDescending(x => x.Count())
                .Take(2)
                .Select(group => new
                 {
                     name = group.Key,
                     cars = group.ToList()
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
