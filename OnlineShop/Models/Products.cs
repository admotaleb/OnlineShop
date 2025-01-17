﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        
        public string Image { get; set; }
        [Display(Name = "Product Color")]
        public string ProductColor { get; set; }
        [Display(Name ="Available")]
        public bool IsAvailable { get; set; }
        [Display(Name ="Product Type")]
        [Required]
        public int ProductTypeId { get; set; }
        [Required]
        [ForeignKey("ProductTypeId")]

        public ProductTypes productTypes { get; set; }

        [Display(Name = "Special Tag")]
        public int SpecialTagId { get; set; }
        [Display(Name ="Special Tag")]
        [ForeignKey("SpecialTagId")]

        public SpecialTags specialTags { get; set; }
    }
}
