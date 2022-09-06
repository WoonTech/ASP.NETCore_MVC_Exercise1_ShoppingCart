using System.ComponentModel.DataAnnotations;

namespace ASP.NETCore_MVC__Exercise1_ShoppingCart.Models
{
    public class Page
    {
        public int Id { get; set; }
        [Required,MinLength(2,ErrorMessage = "Minimum length is 2")]
        [Display(Name = "Header")]
        public string Title { get; set; }      
        
        [Required,MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Content { get; set; } 
        public int Sorting { get; set; }
        public string? Slug { get; set; }
    }
}
