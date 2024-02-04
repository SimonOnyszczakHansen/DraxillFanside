using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace DraxillFanside.Pages
{
    // Manages user login functionality.
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        // Attempts to log in the user with the provided username and password.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Return with errors if the model state is invalid.
            }

            var user = UserStore.GetUser(Username);

            // Verify the user exists and the password matches.
            if (user != null && user.Password == Password)
            {
                HttpContext.Session.SetString("username", Username);
                HttpContext.Session.SetString("role", user.Role ?? "user"); // Default to "user" role if none specified.

                return RedirectToPage("/Index");
            }

            // If login fails, show an error message.
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return Page();
        }
    }
}
