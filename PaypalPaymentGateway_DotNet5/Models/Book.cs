using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaypalPaymentGateway_DotNet5.Models
{
    public class Book
    {
        [Key]
        public Guid Id { set; get; }
        public string Title { set; get; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Thumbnail { set; get; }
        public double Price { set; get; }
    }
}
