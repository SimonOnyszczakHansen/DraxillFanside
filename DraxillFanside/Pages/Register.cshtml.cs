using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace DraxillFanside.Pages
{
    // Handles user registration functionality.
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        // Processes the registration request.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || Password != ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Invalid registration details or passwords do not match.");
                return Page();
            }

            if (UserStore.UserExists(Username))
            {
                ModelState.AddModelError(string.Empty, "Username already exists");
                return Page();
            }

            UserStore.AddUser(new User { Username = Username, Password = Password });
            return RedirectToPage("/Login");
        }
    }
}
