﻿using System.ComponentModel.DataAnnotations;

namespace ASP.NETCore_MVC__Exercise1_ShoppingCart.Models
{
    public class Page
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public string Content { get; set; } 
        public int Sorting { get; set; }    
    }
}