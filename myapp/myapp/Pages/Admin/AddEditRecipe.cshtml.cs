using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace myapp.Pages.Admin
{
    public class AddEditRecipeModel : PageModel
    {
        [FromRoute]
        public long? ID { get; set; }

        public bool IsNewRecipe
        {
            get { return ID == null; }
        }
        public void OnGet()
        {

        }
    }
}