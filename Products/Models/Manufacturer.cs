using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        [StringLength(maximumLength:40)]
        public string Name { get; set; }
     
    }
}