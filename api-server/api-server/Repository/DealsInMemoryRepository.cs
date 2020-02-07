using api_server.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_server.Repository
{
    public class DealsInMemoryRepository
    {

        private static readonly ConcurrentBag<Deal> ObjectList = new ConcurrentBag<Deal>();
        
        public int Add(Deal obj)
        {
            var context = new ValidationContext(obj, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(obj, context, results, true))
            {
                ObjectList.Add(obj);
            }
            return obj.DealNumber;
        }

        public Deal Get(int id)
        {
            return ObjectList.FirstOrDefault(n => n.DealNumber == id);
        }

        public List<Deal> GetAll(Func<Deal, bool> predicate)
        {
            return ObjectList.Where(predicate).ToList();
        }

        public List<Deal> GetAll()
        {
            return ObjectList.ToList();
        }

        public IQueryable<Deal> Query()
        {
            return GetAll().AsQueryable();
        }
    }
}
