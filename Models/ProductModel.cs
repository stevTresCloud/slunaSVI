using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slunaSVI.Models
{
    public class ProductModel
    {
        [Key]
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double? UnitPrice { get; set; }

        public ProductModel() { }

        public ProductModel(string id, string productName, string productDescription, double? unitPrice)
        {
            Id = id;
            ProductName = productName;
            ProductDescription = productDescription;
            UnitPrice = unitPrice;
        }
    }
}
