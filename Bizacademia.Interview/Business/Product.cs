using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizacademia.Interview.Business
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double Cost { get; set; }
        public string DiscountCode { get; set; }
        public double DiscountPercentage { get; set; }
        public double DiscountPrice { get; set; }

        public Product(string productId, string productName, double cost, string discountCode, double discountPercentage)
        {
            ProductId = productId;
            ProductName = productName;
            Cost = cost;
            DiscountCode = discountCode;
            DiscountPercentage = discountPercentage;
            DiscountPrice = cost - cost * discountPercentage / 100;
        }
    }
}
