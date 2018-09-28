using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace myapp.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public IActionResult OnPost()
        {
            var isVaildUser =
                EmailAddress == "w"
             && Password == "1";
            if (!isVaildUser)
            {
                ModelState.AddModelError("haha dumb dumb", "get off my lawn");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //this two line what the fuck ??????????????? he say he not going to explain in this course because is out of scrope so 
            // for now just use it????????????????
            // fucking black magic ????????????
            //it doesnt work in 2.1 but i think is working for 2.0
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                        new[] { new Claim(ClaimTypes.Name, EmailAddress) },
                        scheme
                    )
                );
            return SignIn(user, scheme);

        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            await HttpContext.SignOutAsync(scheme);
            return RedirectToPage("/Index");
        }
    }
}