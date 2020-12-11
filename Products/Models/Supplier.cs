using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Products.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }

        public Supplier()
        {
            Products = new List<Product>();
        }
    }
}