using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public string Description { get; set; } = String.Empty;

        [Required]
        public Double Price { get; set; }

        [ValidateNever]
        public String? ImageUrl { get; set; }

        public int CategoryId { get; set; }

        [ValidateNever]
        public virtual Category? Category { get; set; }
    }
}