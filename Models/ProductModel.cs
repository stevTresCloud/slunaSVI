using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace slunaSVI.Models
{
    public class ProductModel
    {
        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("productDescription")]
        public string ProductDescription { get; set; }

        [JsonProperty("unitPrice")]
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
