using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_server.Models
{
    public class PostDealResponse
    {
        public List<Deal> Deals { set; get; }
        public object TopDeals { set; get; }
    }
}
