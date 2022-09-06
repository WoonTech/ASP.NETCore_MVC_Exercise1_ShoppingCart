using ASP.NETCore_MVC__Exercise1_ShoppingCart.Controllers.Infrastructure;
using ASP.NETCore_MVC__Exercise1_ShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCore_MVC__Exercise1_ShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly ShoppingCartContext context;
        public PagesController(ShoppingCartContext context)
        {
            this.context = context;
        }

        //GET request/admin/pages
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in context.Pages orderby p.Sorting select p;
            List<Page> pagesList = await pages.ToListAsync();
            return View(pagesList);
        }

        //GET request/admin/pages/details/5
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
        //Get /admin/pages/create
        public IActionResult Create() => View();

        //Post /admin/pages/create
        [HttpPost]
        [ValidateAntiForgeryToken]//csr protection (cross site request forgery)
        public async Task<IActionResult> Create(Page page) //IActionResult allow to return both view and RedirectToAction in the same method
        {
            //model binding - pass data to the method/action
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ","-");
                page.Sorting = 100;
                var slug = await context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if(slug != null)
                {
                    ModelState.AddModelError("","The page already exists.");
                    return View(page);
                }
                context.Add(page);
                await context.SaveChangesAsync();

                //display flash page by using partial view
                TempData["Success"] = "The page has been added!";

                return RedirectToAction("Index");
            }
            return View(page);
        }
        //GET request/admin/pages/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Page page = await context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
        //Post /admin/pages/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            //model binding - pass data to the method/action
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");

                var slug = await context.Pages.Where(x => x.Id != page.Id).FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The page already exists.");
                    return View(page);
                }
                context.Update(page);
                await context.SaveChangesAsync();

                //display flash page by using partial view
                TempData["Success"] = "The page has been edited!";

                return RedirectToAction("Index",new {id = page.Id});
            }
            return View(page);
        }
        //GET request/admin/pages/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Page page = await context.Pages.FindAsync(id);
            if (page == null)
            {
                TempData["Error"] = "The page does not exist!";
            }
            else 
            {
                context.Pages.Remove(page);
                await context.SaveChangesAsync();
                TempData["Success"] = "The page has been deleted!";
            }
            return RedirectToAction("Index");
        }
    }
}
