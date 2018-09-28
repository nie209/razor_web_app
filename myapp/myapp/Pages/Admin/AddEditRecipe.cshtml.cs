using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myapp.Models;

namespace myapp.Pages.Admin
{
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
        public IFormFile image { get; set; }


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
            recipe.Name = Recipe.Name;
            recipe.Description = Recipe.Description;
            recipe.Directions = Recipe.Directions;
            recipe.Ingredients = Recipe.Ingredients;
            if(image != null)
            {
                using(var stream = new System.IO.MemoryStream())
                {
                    await image.CopyToAsync(stream);
                    recipe.Image = stream.ToArray();
                    recipe.ImageContentType = image.ContentType;
                }
            }
            await recipesService.SaveAsync(Recipe);
            return RedirectToPage("/Recipe", new { id=Recipe.Id });
        }
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await recipesService.DeleteAsync(ID.Value);
            return RedirectToPage("/Index");

        }
         
    }
}