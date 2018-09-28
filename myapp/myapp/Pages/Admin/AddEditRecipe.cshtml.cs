using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myapp.Models;

namespace myapp.Pages.Admin
{
    //[Authorize] // lock the page down 
    public class AddEditRecipeModel : PageModel
    {
        private readonly IRecipesService recipesService;
        [FromRoute]
        public long? ID { get; set; }
        public bool IsNewRecipe
        {
            get { return ID == null; }
        }
        [BindProperty]
        public Recipe Recipe { get; set; }
        [BindProperty]
        public IFormFile MyImage { get; set; }


        public AddEditRecipeModel(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        public async Task OnGetAsync()
        {

            Recipe = await recipesService.FindAsync(ID.GetValueOrDefault()) ?? new Recipe();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var recipe = await recipesService.FindAsync(ID.GetValueOrDefault()) ?? new Recipe();
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            recipe.Name = Recipe.Name;
            recipe.Description = Recipe.Description;
            recipe.Directions = Recipe.Directions;
            recipe.Ingredients = Recipe.Ingredients;
            if(MyImage != null)
            {
                using(var stream = new System.IO.MemoryStream())
                {
                    await MyImage.CopyToAsync(stream);
                    recipe.Image = stream.ToArray();
                    recipe.ImageContentType = MyImage.ContentType;
                }
            }
            await recipesService.SaveAsync(recipe);
            return RedirectToPage("/Recipe", new { id=recipe.Id });
        }
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await recipesService.DeleteAsync(ID.Value);
            return RedirectToPage("/Index");

        }
         
    }
}