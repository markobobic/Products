using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Products.ViewModels
{
    public class ProductAddViewModel
    {
        [StringLength(maximumLength:50)]
        [Required]
        public string Name { get; set; }
        [StringLength(maximumLength:250)]
        public string Description { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Only numbers allowed")]
        [Range(minimum:0,maximum:999999,ErrorMessage ="Only allowed from 0 to 999999")]
        public double Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public int SupplierId { get; set; }
    }
}