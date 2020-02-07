using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace api_server.Models
{
    public class Deal
    {
        [Required]
        [Key]
        public int DealNumber { set; get; }
        [Required]
        public string CustomerName { set; get; }
        [Required]
        public string DealershipName { set; get; }
        [Required]
        public string Vehicle { set; get; }
        [Required]
        public double Price { set; get; }
        [Required]
        public DateTime Date { set; get; }
    }
}
