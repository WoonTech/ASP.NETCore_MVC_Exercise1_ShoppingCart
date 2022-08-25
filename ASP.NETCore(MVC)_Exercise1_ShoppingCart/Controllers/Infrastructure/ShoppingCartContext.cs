using ASP.NETCore_MVC__Exercise1_ShoppingCart.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCore_MVC__Exercise1_ShoppingCart.Controllers.Infrastructure
{
    public class ShoppingCartContext:DbContext
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options):base(options)
        {
            
        }
        public DbSet<Page> Pages { get; set; } 

    }
}
